# 🦈 GameShark Systems v1.0.4

> **Status:** Otimizado para Operações de Campo (Produção) 🟢
> **Objetivo:** Plataforma definitiva para catálogo, venda e gestão de loot digital e físico.

---

## 🛠️ Tech Stack & Arquitetura

O ecossistema **GameShark** utiliza uma arquitetura multicamadas (Clean Architecture) para garantir escalabilidade e separação de responsabilidades:

* **Core:** .NET 9 (C#)
* **Web UI:** ASP.NET Core Razor Pages + Bootstrap 5 (Neon Theme)
* **API:** RESTful Services para integração Multi-plataforma
* **Desktop:** .NET WPF/WinForms (Módulo de Caixa/PDV)
* **Database:** Entity Framework Core + SQL Server
* **Security:** ASP.NET Core Identity + Auditoria Customizada

---

## 🚀 Funcionalidades Principais

### 🌐 Web & E-Commerce
- **Catálogo Inteligente:** Filtros por plataforma e categoria com paginação.
- **Carrinho Persistente:** O seu loot não desaparece entre sessões.
- **Área Admin:** CRUD completo de produtos, categorias e gestão de usuários.
- **Radar de Inventário:** Monitoramento em tempo real dos itens em estoque.

### 🖥️ Desktop (Terminal de Operações)
- **Operação de Caixa:** Registro de vendas rápidas via API.
- **Sincronização:** Comunicação direta com a base central para controle de pedidos.

### 🔐 Segurança & Auditoria
- **Logs de Segurança:** Rastreamento completo de ações administrativas.
- **ImageSecurity:** Validação e sanitização de uploads de mídia.
- **Identity localized:** Sistema de autenticação totalmente em PT-BR.

---

## 📂 Estrutura do Projeto

```text
GameShark/
├── GameShark.Domain          # Entidades e Regras de Negócio
├── GameShark.Application     # DTOs, Interfaces e Serviços de Consulta
├── GameShark.Infrastructure  # DBContext, Repositórios e Migrations
├── GameShark.Api             # Endpoints REST para Integração
├── GameShark.Web             # Interface Web e Painel Administrativo
└── GameShark.Desktop         # Cliente Windows para Frente de Caixa