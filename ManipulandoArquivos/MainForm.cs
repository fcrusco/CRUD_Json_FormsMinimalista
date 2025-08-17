using System.Text.Json;

namespace CrudJsonWinForms
{
    public class Pessoa
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = "";
        public string Sobrenome { get; set; } = "";
        public override string ToString() => $"{Nome} {Sobrenome}";
    }

    public class MainForm : Form
    {
        // Controles
        private readonly TextBox txtNome = new() { PlaceholderText = "Nome" };
        private readonly TextBox txtSobrenome = new() { PlaceholderText = "Sobrenome" };
        private readonly Button btnNovo = new() { Text = "Novo" };
        private readonly Button btnSalvar = new() { Text = "Salvar" };
        private readonly Button btnExcluir = new() { Text = "Excluir" };
        private readonly ListBox lstPessoas = new() { IntegralHeight = false };

        // Estado
        private readonly string pastaDados;
        private readonly string caminhoJson;
        private readonly JsonSerializerOptions jsonOpts = new() { WriteIndented = true };
        private List<Pessoa> pessoas = new();
        private Guid? editandoId = null;

        public MainForm()
        {
            Text = "CRUD JSON - Pessoas (Nome/Sobrenome)";
            Width = 560;
            Height = 420;
            StartPosition = FormStartPosition.CenterScreen;

            // Caminhos (pasta do exe /data/pessoas.json)
            pastaDados = Path.Combine(AppContext.BaseDirectory, "data");
            caminhoJson = Path.Combine(pastaDados, "pessoas.json");
            Directory.CreateDirectory(pastaDados);

            // Layout simples (TableLayoutPanel)
            var grid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 6,
                Padding = new Padding(10)
            };
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            // 0: Nome, 1: Sobrenome, 2: Botões, 3: ListBox (preenche), 4: espaço extra
            grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 35)); // Nome
            grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 35)); // Sobrenome
            grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 40)); // Botões
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // ListBox ocupa o resto
            grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 1));  // espaço mínimo

            var lblNome = new Label { Text = "Nome:", AutoSize = true, Anchor = AnchorStyles.Left };
            var lblSobrenome = new Label { Text = "Sobrenome:", AutoSize = true, Anchor = AnchorStyles.Left };
            txtNome.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtSobrenome.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            // Linha botões
            var pnlBtns = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight };
            btnNovo.Width = 100;
            btnSalvar.Width = 100;
            btnExcluir.Width = 100;
            pnlBtns.Controls.AddRange(new Control[] { btnNovo, btnSalvar, btnExcluir });

            // Listbox
            lstPessoas.Dock = DockStyle.Fill;

            // Adiciona no grid
            grid.Controls.Add(lblNome, 0, 0);
            grid.Controls.Add(txtNome, 1, 0);
            grid.Controls.Add(lblSobrenome, 0, 1);
            grid.Controls.Add(txtSobrenome, 1, 1);
            grid.Controls.Add(pnlBtns, 0, 2);
            grid.SetColumnSpan(pnlBtns, 2);
            grid.Controls.Add(lstPessoas, 0, 3);
            grid.SetColumnSpan(lstPessoas, 2);

            Controls.Add(grid);

            // Eventos
            Load += (_, __) => CarregarJson();
            btnNovo.Click += (_, __) => LimparFormulario();
            btnSalvar.Click += (_, __) => SalvarOuAtualizar();
            btnExcluir.Click += (_, __) => ExcluirSelecionado();
            lstPessoas.DoubleClick += (_, __) => CarregarParaEdicao();
            lstPessoas.SelectedIndexChanged += (_, __) => SincronizarSelecao();

            // Dica de uso
            ToolTip tip = new();
            tip.SetToolTip(lstPessoas, "Dê duplo clique em um item para editar.");
        }

        private void CarregarJson()
        {
            try
            {
                if (File.Exists(caminhoJson))
                {
                    var json = File.ReadAllText(caminhoJson);
                    pessoas = JsonSerializer.Deserialize<List<Pessoa>>(json) ?? new List<Pessoa>();
                }
                else
                {
                    pessoas = new List<Pessoa>();
                    SalvarJson(); // cria arquivo vazio na primeira execução
                }
                RecarregarLista();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar JSON: {ex.Message}");
            }
        }

        private void SalvarJson()
        {
            var json = JsonSerializer.Serialize(pessoas, jsonOpts);
            File.WriteAllText(caminhoJson, json);
        }

        private void RecarregarLista()
        {
            lstPessoas.BeginUpdate();
            lstPessoas.Items.Clear();
            foreach (var p in pessoas.OrderBy(p => p.Nome).ThenBy(p => p.Sobrenome))
                lstPessoas.Items.Add(p);
            lstPessoas.EndUpdate();
        }

        private void LimparFormulario()
        {
            editandoId = null;
            txtNome.Clear();
            txtSobrenome.Clear();
            txtNome.Focus();
            lstPessoas.ClearSelected();
        }

        private bool ValidarFormulario(out string msg)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                msg = "Informe o Nome.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSobrenome.Text))
            {
                msg = "Informe o Sobrenome.";
                return false;
            }
            msg = "";
            return true;
        }

        private void SalvarOuAtualizar()
        {
            if (!ValidarFormulario(out var msg))
            {
                MessageBox.Show(msg);
                return;
            }

            if (editandoId is Guid id)
            {
                // Atualiza
                var p = pessoas.FirstOrDefault(x => x.Id == id);
                if (p != null)
                {
                    p.Nome = txtNome.Text.Trim();
                    p.Sobrenome = txtSobrenome.Text.Trim();
                }
                MessageBox.Show("Registro atualizado.");
            }
            else
            {
                // Novo
                var p = new Pessoa
                {
                    Nome = txtNome.Text.Trim(),
                    Sobrenome = txtSobrenome.Text.Trim()
                };
                pessoas.Add(p);
                MessageBox.Show("Registro incluído.");
            }

            SalvarJson();
            RecarregarLista();
            LimparFormulario();
        }

        private void ExcluirSelecionado()
        {
            if (lstPessoas.SelectedItem is not Pessoa sel)
            {
                MessageBox.Show("Selecione um registro para excluir.");
                return;
            }

            if (MessageBox.Show($"Excluir '{sel}'?", "Confirmação",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                pessoas.RemoveAll(x => x.Id == sel.Id);
                SalvarJson();
                RecarregarLista();
                LimparFormulario();
            }
        }

        private void CarregarParaEdicao()
        {
            if (lstPessoas.SelectedItem is not Pessoa sel) return;
            editandoId = sel.Id;
            txtNome.Text = sel.Nome;
            txtSobrenome.Text = sel.Sobrenome;
            txtNome.Focus();
        }

        private void SincronizarSelecao()
        {
            // Mostra dados nos campos quando trocar a seleção (sem exigir duplo clique)
            if (lstPessoas.SelectedItem is Pessoa sel)
            {
                editandoId = sel.Id;
                txtNome.Text = sel.Nome;
                txtSobrenome.Text = sel.Sobrenome;
            }
        }
    }
}
