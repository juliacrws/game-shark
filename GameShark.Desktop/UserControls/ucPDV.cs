using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GameShark.Desktop.Services;

namespace GameShark.Desktop.UserControls;

public partial class ucPDV : UserControl
{
    private readonly VendaApiService _apiService;
    private List<ProdutoVendaModel> _catalogoCompleto; // Memória do catálogo
    private readonly List<ItemCarrinhoModel> _carrinho;

    public ucPDV()
    {
        InitializeComponent();
        _apiService = new VendaApiService();
        _catalogoCompleto = new List<ProdutoVendaModel>();
        _carrinho = new List<ItemCarrinhoModel>();

        // Eventos
        this.Load += UcPDV_Load;
        txtBuscaProduto.TextChanged += TxtBuscaProduto_TextChanged;
        lstCatalogo.DoubleClick += LstCatalogo_DoubleClick;
        lstCarrinho.DoubleClick += LstCarrinho_DoubleClick; // 👈 Adiciona essa linha
        btnFinalizar.Click += BtnFinalizar_Click;
    }

    // 🚀 MÁGICA 1: Quando a tela abre, puxa tudo do banco!
    private async void UcPDV_Load(object? sender, EventArgs e)
    {
        lstCatalogo.Items.Add("Carregando matriz de dados...");

        _catalogoCompleto = await _apiService.ObterCatalogoDaApiAsync();

        AtualizarCatalogo(_catalogoCompleto);
    }

    // 🚀 MÁGICA 2: Filtra a lista ao vivo enquanto o operador digita
    private void TxtBuscaProduto_TextChanged(object? sender, EventArgs e)
    {
        string termo = txtBuscaProduto.Text.ToLower();

        var produtosFiltrados = _catalogoCompleto
            .Where(p => p.Nome.ToLower().Contains(termo) || p.Id.ToString() == termo)
            .ToList();

        AtualizarCatalogo(produtosFiltrados);
    }

    // Preenche a lista da esquerda (Vitrine)
    private void AtualizarCatalogo(List<ProdutoVendaModel> produtos)
    {
        lstCatalogo.Items.Clear();
        foreach (var p in produtos)
        {
            // Salva o objeto inteiro na lista visual pra podermos resgatar no duplo clique
            lstCatalogo.Items.Add(p);
        }

        // Define que a lista vai mostrar a propriedade "Nome" do objeto ProdutoVendaModel
        lstCatalogo.DisplayMember = "Nome";
    }

    // 🚀 MÁGICA 3: Duplo clique joga pro carrinho!
    private void LstCatalogo_DoubleClick(object? sender, EventArgs e)
    {
        if (lstCatalogo.SelectedItem is ProdutoVendaModel produtoSelecionado)
        {
            _carrinho.Add(new ItemCarrinhoModel { Produto = produtoSelecionado, Quantidade = 1 });
            AtualizarCarrinho();
        }
    }

    // Preenche a lista da direita (Carrinho) e soma o Total
    private void AtualizarCarrinho()
    {
        lstCarrinho.Items.Clear();
        foreach (var item in _carrinho)
        {
            lstCarrinho.Items.Add($"1x {item.Produto.Nome} - R$ {item.Subtotal:F2}");
        }

        decimal total = _carrinho.Sum(i => i.Subtotal);
        lblTotalValor.Text = $"{total:F2}";
    }

    private async void BtnFinalizar_Click(object? sender, EventArgs e)
    {
        if (!_carrinho.Any()) return;

        decimal total = _carrinho.Sum(i => i.Subtotal);
        btnFinalizar.Enabled = false;
        btnFinalizar.Text = "PROCESSANDO...";

        bool sucesso = await _apiService.FinalizarVendaAsync(total, _carrinho); // 👈 Passando o carrinho!
        if (sucesso)
        {
            MessageBox.Show("Venda registrada! Obrigado por comprar na GameShark.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _carrinho.Clear();
            AtualizarCarrinho();
        }
        else
        {
            MessageBox.Show("Erro ao comunicar com o servidor.", "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        btnFinalizar.Text = "FINALIZAR VENDA";
        btnFinalizar.Enabled = true;
    }
    // 🗑️ MÁGICA 4: Duplo clique no carrinho REMOVE o item!
    private void LstCarrinho_DoubleClick(object? sender, EventArgs e)
    {
        // Verifica se clicou num item válido
        int index = lstCarrinho.SelectedIndex;

        if (index >= 0 && index < _carrinho.Count)
        {
            // Pega o nome do item para mostrar na mensagem
            string nomeRemovido = _carrinho[index].Produto.Nome;

            // Remove da nossa lista (memória)
            _carrinho.RemoveAt(index);

            // Manda o carrinho se desenhar de novo na tela
            AtualizarCarrinho();

            // (Opcional) Mostra um aviso rápido na tela
            // MessageBox.Show($"{nomeRemovido} removido do carrinho!", "Aviso");
        }
    }
}