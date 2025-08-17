# CRUD JSON Minimalista em C# WinForms

Este projeto consiste em uma aplicação **Windows Forms** desenvolvida em **C# (.NET 8)** que implementa um **CRUD (Create, Read, Update, Delete)** simples utilizando um arquivo **JSON** como base de dados.  

<img width="671" height="506" alt="image" src="https://github.com/user-attachments/assets/b0d4beb8-1c7c-4856-aef4-585867f225b2" />

---

## 🇧🇷 Português (Brasil)

### Descrição do Projeto – CRUD JSON Minimalista em C# WinForms

Este projeto consiste em uma aplicação **Windows Forms** desenvolvida em **C# (.NET 8)** que implementa um **CRUD (Create, Read, Update, Delete)** simples utilizando um arquivo **JSON** como base de dados.  

#### Funcionalidades
- **Cadastro** de pessoas com os campos: **Nome** e **Sobrenome**.  
- **Listagem** de todos os registros em um **ListBox** de fácil visualização.  
- **Edição** de registros através de **duplo clique** na lista.  
- **Exclusão** de registros com confirmação.  
- **Armazenamento persistente** em arquivo JSON (`pessoas.json`), garantindo que os dados não sejam perdidos entre execuções.  
- O arquivo JSON é armazenado em uma pasta específica `data`, no mesmo nível do executável.  

#### Detalhes Técnicos
- Desenvolvido em **C# com .NET 8**.  
- Interface construída em **Windows Forms** com **layout minimalista**, utilizando `TableLayoutPanel` para organização dos controles.  
- Serialização e desserialização feitas com a biblioteca nativa `System.Text.Json`.  
- Estrutura de dados representada pela classe `Pessoa`, contendo um identificador `Guid`, `Nome` e `Sobrenome`.  
- Código estruturado de forma clara e didática, ideal para **aulas e demonstrações práticas**.  

O estilo **minimalista** do projeto elimina elementos visuais complexos, mantendo apenas os controles essenciais: campos de entrada, botões de ação e a lista de registros. Isso reforça o foco nos conceitos principais de manipulação de arquivos JSON e operações CRUD em uma interface gráfica simples.  

---

## 🇺🇸 English

### Project Description – Minimalist JSON CRUD in C# WinForms

This project is a **Windows Forms application** built with **C# (.NET 8)** that implements a simple **CRUD (Create, Read, Update, Delete)** using a **JSON file** as the data store.  

#### Features
- **Register** people with two fields: **First Name** and **Last Name**.  
- **List** all records in a clean and easy-to-read **ListBox**.  
- **Edit** records via **double-click** on the list.  
- **Delete** records with confirmation.  
- **Persistent storage** in a JSON file (`pessoas.json`), ensuring data remains available between sessions.  
- JSON file is stored in a dedicated `data` folder, at the same level as the executable.  

#### Technical Details
- Developed in **C# with .NET 8**.  
- UI built in **Windows Forms** with a **minimalist layout**, using `TableLayoutPanel` to arrange controls.  
- JSON serialization and deserialization handled by native `System.Text.Json`.  
- Data model represented by a `Pessoa` class containing a `Guid` identifier, `FirstName` and `LastName`.  
- Code organized in a clean and didactic way, making it ideal for **teaching and practical demonstrations**.  

The **minimalist style** of the project avoids unnecessary visual elements, keeping only the essential controls: input fields, action buttons, and the record list. This approach emphasizes the core concepts of JSON file manipulation and CRUD operations within a simple graphical interface.  

---

## 🇪🇸 Español

### Descripción del Proyecto – CRUD JSON Minimalista en C# WinForms

Este proyecto es una aplicación de **Windows Forms** desarrollada en **C# (.NET 8)** que implementa un **CRUD (Crear, Leer, Actualizar, Eliminar)** sencillo utilizando un archivo **JSON** como base de datos.  

#### Funcionalidades
- **Registro** de personas con dos campos: **Nombre** y **Apellido**.  
- **Listado** de todos los registros en un **ListBox** de fácil lectura.  
- **Edición** de registros mediante **doble clic** en la lista.  
- **Eliminación** de registros con confirmación.  
- **Almacenamiento persistente** en archivo JSON (`pessoas.json`), garantizando la conservación de los datos entre ejecuciones.  
- El archivo JSON se guarda en una carpeta específica `data`, al mismo nivel que el ejecutable.  

#### Detalles Técnicos
- Desarrollado en **C# con .NET 8**.  
- Interfaz gráfica construida con **Windows Forms**, adoptando un **diseño minimalista** mediante `TableLayoutPanel`.  
- Serialización y deserialización realizadas con la biblioteca nativa `System.Text.Json`.  
- Modelo de datos representado por la clase `Pessoa`, que contiene un identificador `Guid`, `Nombre` y `Apellido`.  
- Código organizado de manera clara y didáctica, ideal para **clases y demostraciones prácticas**.  

El estilo **minimalista** del proyecto evita elementos visuales innecesarios, conservando únicamente los controles esenciales: campos de entrada, botones de acción y la lista de registros. Esto refuerza el enfoque en los conceptos fundamentales de manipulación de archivos JSON y operaciones CRUD en una interfaz gráfica sencilla.  
