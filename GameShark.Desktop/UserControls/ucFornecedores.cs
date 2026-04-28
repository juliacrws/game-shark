using System;
using System.Windows.Forms;
using GameShark.Desktop.Services;

namespace GameShark.Desktop.UserControls
{
    public partial class ucFornecedores : UserControl
    {
        private readonly FornecedorApiService _apiService;

        // Construtor
        public ucFornecedores()
        {
            InitializeComponent();

            // Aqui você instancia o HttpClient. 
            // (Ajuste a URL base para a porta que sua API roda)
            var httpClient = new System.Net.Http.HttpClient { BaseAddress = new Uri("https://localhost:7000/") };
            _apiService = new FornecedorApiService(httpClient);
        }

        // Evento de quando a tela abre
        private async void ucFornecedores_Load(object sender, EventArgs e)
        {
            await CarregarGrid();
        }

        private async System.Threading.Tasks.Task CarregarGrid()
        {
            try
            {
                var fornecedores = await _apiService.ObterTodosAsync();
                dgvFornecedores.DataSource = fornecedores;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar matriz de fornecedores: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            var fornecedor = new FornecedorDto
            {
                NomeFantasia = txtNome.Text,
                Cnpj = txtCnpj.Text,
                Telefone = txtTelefone.Text,
                Email = txtEmail.Text
            };

            bool sucesso;

            // Se o txtId estiver vazio, é um CREATE. Se tiver número, é um UPDATE.
            if (string.IsNullOrEmpty(txtId.Text))
            {
                sucesso = await _apiService.CriarAsync(fornecedor);
            }
            else
            {
                fornecedor.Id = int.Parse(txtId.Text);
                sucesso = await _apiService.AtualizarAsync(fornecedor.Id, fornecedor);
            }

            if (sucesso)
            {
                MessageBox.Show("Dados salvos no Mainframe com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimparCampos();
                await CarregarGrid();
            }
            else
            {
                MessageBox.Show("Erro ao salvar dados. Verifique a conexão.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnExcluir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Selecione um fornecedor para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacao = MessageBox.Show("Deseja mesmo apagar esse registro do sistema?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacao == DialogResult.Yes)
            {
                int id = int.Parse(txtId.Text);
                var sucesso = await _apiService.DeletarAsync(id);

                if (sucesso)
                {
                    MessageBox.Show("Registro deletado.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                    await CarregarGrid();
                }
            }
        }

        // Quando clica numa linha do Grid, joga os dados para as TextBoxes para poder editar
        private void dgvFornecedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvFornecedores.Rows[e.RowIndex];

                txtId.Text = row.Cells["Id"].Value?.ToString();
                txtNome.Text = row.Cells["NomeFantasia"].Value?.ToString();
                txtCnpj.Text = row.Cells["Cnpj"].Value?.ToString();
                txtTelefone.Text = row.Cells["Telefone"].Value?.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            txtId.Clear();
            txtNome.Clear();
            txtCnpj.Clear();
            txtTelefone.Clear();
            txtEmail.Clear();
        }
    }
}