using System;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using binho.Model;
using System.IO;
using System.Drawing;
using System.Linq;
namespace binho
{
    public partial class frmPrincipal : System.Windows.Forms.Form
    {
        //Lista com os locais  a serem cadastrados
        private List<Local> locais = new List<Local>();
        //Lista com os viajantes  a serem cadastrados
        private List<Viajante> viajantes = new List<Viajante>();
        //Lista com as viagens cadastradas
        private List<Viagem> viagens = new List<Viagem>();



        //objeto de local para saber o que esta sendo alterado ou excluido
        private Local current_local;
        //objeto de viajante para saber o que esta sendo alterado ou excluido
        private Viajante current_Viajante;
        //obejeto de viagem para saber oque esta sendo alterado ou excluido
        private Viagem current_viagem;

        public frmPrincipal()
        {
            InitializeComponent();

        }
        //ABA de Viagem\\
        #region Aba de Viagem


        private void btnUpFoto_Click(object sender, EventArgs e)
        {
            if (cmbviagem.Text == "")
            {
                MessageBox.Show("Selecione uma Viagem para Atualizar");
            }

            else
            {
                picBFotoViagemup.ImageLocation = "";

                OpenFileDialog picviagemup = new OpenFileDialog();
                picviagemup.Title = "Escolha uma foto para o viajante";
                picviagemup.Filter = "JPeg|*.jpg|Bitmap|*.bmp|Gif |*.gif";
                picviagemup.ShowDialog();
                picBFotoViagemup.ImageLocation = picviagemup.FileName;
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {


            if (txtIdViagem.Text != "" && txtDescViagem.Text != "" && cmbViajanteLink.Text != "" && cmbLinkLocal.Text != "" && cmbClassViagem.Text != picBFotoViagem.ImageLocation)
            {

                cmbviagem.Items.Clear();
                //Declaração da variavel temporaria do objeto viagem
                Viagem viagem = new Viagem();

                viagem.iD = Convert.ToInt32(txtIdViagem.Text);
                viagem.descricao = txtDescViagem.Text;
                viagem.foto = picBFotoViagem.ImageLocation.ToString();
                viagem.classificacao = Convert.ToInt32(cmbClassViagem.Text);

                //atribuindo o valor do item selecionado
                string nome = cmbLinkLocal.SelectedItem.ToString();
                foreach (Local local_item in locais)
                {
                    if (local_item.Nome.Equals(nome))
                    {
                        viagem.local = local_item;
                        break;
                    }



                }
                viagens.Add(viagem);
                foreach (Viajante viajante_item in viajantes)
                {
                    if (viajante_item.Id == Convert.ToInt32(txtIdViagem.Text))
                    {
                        viajante_item.viagens.Add(viagem);
                        break;
                    }
                }


                //Atualiza o listbox de alterar e Excluir com o foreach

                foreach (Viagem viagem_item in viagens)
                {
                    //Configurar-se a string que será colocada na seleção do combolist
                    string texto = viagem_item.iD.ToString();
                    //local_item é a isntancia de cada objeto percorrendo o list global de Locais           
                    cmbviagem.Items.Add(texto);
                }
                txtIdViagem.Text = "";
                txtDescViagem.Text = "";
                picBFotoViagem.ImageLocation = null;
                txtIdViagem.Text = "";
                cmbViajanteLink.Text = null;
                cmbLinkLocal.Text = null;
                cmbClassViagem.Text = null;
            }
            else
            {
                MessageBox.Show("Reveja os dados a serem inseridos");
            }
        }

        private void btnFoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog picviagem = new OpenFileDialog();
            picviagem.Title = "Escolha uma foto para o viajante";
            picviagem.Filter = "JPeg|*.jpg|Bitmap|*.bmp|Gif |*.gif";
            picviagem.ShowDialog();
            picBFotoViagem.ImageLocation = picviagem.FileName;
        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor = cmbviagem.SelectedItem.ToString();

            string[] string_quebrada = valor.Split();
            //A primeira parte tem o Id
            int id = Convert.ToInt32(string_quebrada[0]);
            //percorre o array para achar a referencia do objeto atraves do id

            foreach (Viagem viagem_item in viagens)
            {
                if (viagem_item.iD == id)
                {
                    txtDescViagemEdit.Text = viagem_item.descricao;
                    cmbClassEditViagem.Text = viagem_item.classificacao.ToString();
                    picBFotoViagemup.ImageLocation = viagem_item.foto;
                    current_viagem = viagem_item;
                    break;
                }
            }
        }

        private void cmbViajanteLink_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnUpViagem_Click(object sender, EventArgs e)
        {
            if (cmbviagem.Text == "")
            {
                MessageBox.Show("Selecione a viagem para Atualizar");
            }

            else
            {
                current_viagem.descricao = txtDescViagemEdit.Text;
                current_viagem.classificacao = Convert.ToInt32(cmbClassEditViagem.Text);
                current_viagem.foto = picBFotoViagemup.ImageLocation.ToString();
                cmbviagem.Text = "";

                cmbviagem.Items.Clear();

                //Atualiza o listbox  depois de Excluir com o foreach
                foreach (Viagem viagens_item in viagens)
                {
                    //Configurar-se a string que será colocada na seleção do comboBox
                    string texto = viagens_item.iD.ToString();
                    //local_item é a isntancia de cada objeto percorrendo o list global de Locais           
                    cmbviagem.Items.Add(texto);
                }
                //Limpa os dados dos form
                txtDescViagemEdit.Text = "";
                cmbClassEditViagem.Text = null;
                cmbClassEditViagem.Text = "";
                picBFotoViagemup.ImageLocation = "";
            }

        }
        private void btnDellViagem_Click(object sender, EventArgs e)
        {
            if (cmbviagem.Text == "")
            {
                MessageBox.Show("Selecione a Viagem para excluir");
            }
            try
            {
                for (int i = 0; i < cmbviagem.Items.Count; i++)
                {
                    if (cmbviagem.Items[i].ToString() == current_viagem.iD.ToString())
                    {
                        cmbviagem.Items.RemoveAt(i);
                    }

                }

                viagens.Remove(current_viagem);
                txtDescViagemEdit.Text = "";
                cmbClassEditViagem.Text = "";
                picBFotoViagemup.ImageLocation = null;
                cmbClassEditViagem.Text = null;
            }
            catch
            { }

        }

        #endregion

        //ABA de Viajantess\\
        #region Aba de Viajantes

        private void btnCadastrarViajantes_Click(object sender, EventArgs e)
        {
            if (txtIdViajantes.Text != "" && txtNomeViajantes.Text != "" && txtEndViajantes.Text != "" && dtpNascimento.Value < DateTime.Now && picBFotoViajantes.Image != null)
            {
                cmbsetViajantes.Items.Clear();
                //Declaração da variavel temporaria do objeto viajante
                Viajante viajante = new Viajante();
                //Defini o ID
                viajante.Id = Convert.ToInt32(txtIdViajantes.Text);
                //Defini o Nome
                viajante.Nome = txtNomeViajantes.Text;
                //Defini o Endereco
                viajante.Endereco = txtEndViajantes.Text;
                //Defini a data de nascimento
                viajante.Data = dtpNascimento.Value;
                //Defini a foto de viajante
                viajante.Foto = picBFotoViajantes.ImageLocation.ToString();
                //Salvar o objeto setado depois de definir suas propriedades na variavel Global viajantes 
                viajantes.Add(viajante);

                //Atualiza o listbox de alterar e Excluir com o foreach
                foreach (Viajante viajante_item in viajantes)
                {
                    //Configurar-se a string que será colocada na seleção do combolist
                    string texto = viajante_item.Id + " - " + viajante_item.Nome;
                    //local_item é a isntancia de cada objeto percorrendo o list global de Locais           
                    cmbsetViajantes.Items.Add(texto);
                }
                txtEndViajantes.Text = "";
                txtNomeViajantes.Text = "";
                picBFotoViajantes.Image = null;
                txtIdViajantes.Text = "";

                // Atualizar a lista dos nomes dos Viajantes

                cmbViajanteLink.Items.Clear();

                //Atualiza o listbox de alterar e Excluir com o foreach
                cmbViajanteLink.Items.Clear();

                foreach (Viajante viajante_item in viajantes)
                {
                    //Configurar-se a string que será colocada na seleção do comboBox
                    string texto = viajante_item.Nome;
                    //local_item é a isntancia de cada objeto percorrendo o list global de Locais
                    cmbViajanteLink.Items.Add(texto);
                }
            }
            else
            {
                MessageBox.Show("Reveja os dados a serem inseridos");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog save = new OpenFileDialog();
            save.Title = "Escolha uma foto para o viajante";
            save.Filter = "JPeg|*.jpg|Bitmap|*.bmp|Gif |*.gif";
            save.ShowDialog();
            picBFotoViajantes.ImageLocation = save.FileName;
        }

        private void cmbsetViajantes_SelectedIndexChanged(object sender, EventArgs e)

        {
            //atribuindo o valor do item selecionado
            string valor = cmbsetViajantes.SelectedItem.ToString();
            //Quebra a string separado em duas partes atribuindo a um array de string
            string[] string_quebrada = valor.Split();
            //A primeira parte tem o Id
            int id = Convert.ToInt32(string_quebrada[0]);
            //percorre o array para achar a referencia do objeto atraves do id
            foreach (Viajante Viajante_item in viajantes)
            {
                if (Viajante_item.Id == id)
                {
                    txtNameViajanteUp.Text = Viajante_item.Nome;
                    txtEndViajanteUp.Text = Viajante_item.Endereco;
                    dtpNascimentoUp.MaxDate = Viajante_item.Data;
                    picBFotoViajantesUp.ImageLocation = Viajante_item.Foto;
                    current_Viajante = Viajante_item;
                    break;
                }
            }
        }

        private void btnUpViajante_Click(object sender, EventArgs e)
        {
            if (cmbsetViajantes.Text == "")
            {
                MessageBox.Show("Selecione o Viajante Para Atualizar");
            }
            else
            {
                current_Viajante.Nome = txtNameViajanteUp.Text;
                current_Viajante.Endereco = txtEndViajanteUp.Text;
                current_Viajante.Data = dtpNascimentoUp.MaxDate;
                current_Viajante.Foto = picBFotoViajantesUp.ImageLocation;
                cmbsetViajantes.Text = "";


                cmbsetViajantes.Items.Clear();
                //Atualiza o listbox  depois de Excluir com o foreach
                foreach (Viajante viajante_item in viajantes)
                {
                    //Configurar-se a string que será colocada na seleção do combolist
                    string texto = viajante_item.Id + " - " + viajante_item.Nome;
                    //local_item é a isntancia de cada objeto percorrendo o list global de Locais
                    cmbsetViajantes.Items.Add(texto);
                }
                //Limpa os dados dos form
                txtNameViajanteUp.Text = "";
                txtEndViajanteUp.Text = "";
                dtpNascimentoUp.Text = "";
                picBFotoViajantesUp.ImageLocation = "";
            }

            // Atualizar a lista dos nomes dos Viajantes
            cmbViajanteLink.Items.Clear();

            //Atualiza o listbox de alterar e Excluir com o foreach
            cmbViajanteLink.Items.Clear();

            foreach (Viajante viajante_item in viajantes)
            {
                //Configurar-se a string que será colocada na seleção do combobox
                string texto = viajante_item.Nome;
                //local_item é a isntancia de cada objeto percorrendo o list global de Locais
                cmbViajanteLink.Items.Add(texto);
            }

        }

        private void btnDellViajante_Click(object sender, EventArgs e)
        {
            {
                if (cmbsetViajantes.Text == "")
                {
                    MessageBox.Show("Viajante não selecioando");
                }
                try
                {
                    for (int i = 0; i < cmbsetViajantes.Items.Count; i++)
                    {
                        if (cmbsetViajantes.Items[i].ToString() == current_Viajante.Id.ToString() + " - " + current_Viajante.Nome)
                        {
                            cmbsetViajantes.Items.RemoveAt(i);
                        }
                    }
                    viajantes.Remove(current_Viajante);
                    txtEndViajanteUp.Text = "";
                    txtNameViajanteUp.Text = "";
                    picBFotoViajantesUp.ImageLocation = null;
                    dtpNascimentoUp.Value.ToLocalTime();
                    cmbsetViajantes.Text = null;
                }
                catch
                { }

            }
            // Atualizar a lista dos nomes dos Viajantes
            cmbViajanteLink.Items.Clear();

            //Atualiza o listbox de alterar e Excluir com o foreach
            cmbViajanteLink.Items.Clear();

            foreach (Viajante viajante_item in viajantes)
            {
                //Configurar-se a string que será colocada na seleção do combobox
                string texto = viajante_item.Nome;
                //local_item é a isntancia de cada objeto percorrendo o list global de Locais
                cmbViajanteLink.Items.Add(texto);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (cmbsetViajantes.Text == "")
            {
                MessageBox.Show("Selecione o Vijante para Atualizar");
            }

            else {
                picBFotoViajantesUp.ImageLocation = "";

                OpenFileDialog save = new OpenFileDialog();
                save.Title = "Escolha uma foto para o viajante";
                save.Filter = "JPeg|*.jpg|Bitmap|*.bmp|Gif |*.gif";
                save.ShowDialog();
                picBFotoViajantesUp.ImageLocation = save.FileName;

            }
        }

        #endregion

        //ABA de Local\\
        #region Aba de Local

        //Botão para cadastrar o local
        private void btnCadastrarLocal_Click(object sender, EventArgs e)
        {
            //Declaração da variavel temporaria do objeto Local
            Local local = new Local();
            //Pega se os atributos dos controles do form

            if (txtIdLocal.Text != "" && txtNomeLocal.Text != "" && txtCidadeLocal.Text != "")
            {


                int id = Convert.ToInt32(txtIdLocal.Text);
                string nome = txtNomeLocal.Text;
                string cidade = txtCidadeLocal.Text;

                local.Id = id;
                local.Nome = nome;
                local.Cidade = cidade;

                locais.Add(local);

                //Atualiza o listbox de alterar e Excluir com o foreach
                cmbSetLocal.Items.Clear();

                foreach (Local local_item in locais)
                {
                    //Configurar-se a string que será colocada na seleção do comboBOX
                    string texto = local_item.Id + " - " + local_item.Nome;

                    cmbSetLocal.Items.Add(texto);
                }
                //Depois de atualizar limpar os formularios de cadastro
                txtIdLocal.Text = "";
                txtNomeLocal.Text = "";
                txtCidadeLocal.Text = "";

                //Atualiza o combobox do cadastro de viagem
                cmbLinkLocal.Items.Clear();
                foreach (Local local_item in locais)
                {
                    //Configurar-se a string que será colocada na seleção do combolist
                    string texto = local_item.Nome;
                    //local_item é a isntancia de cada objeto percorrendo o list global de Locais
                    cmbLinkLocal.Items.Add(texto);
                }
            }

            else
            {
                MessageBox.Show("Reveja os dados a serem inseridos");
            }
        }

        private void btnUpLocal_Click(object sender, EventArgs e)
        {
            if (cmbSetLocal.Text == "")
            {
                MessageBox.Show("Selecione o Local para atualizar! ");
            }
            else {
                current_local.Nome = txtNomeLocalUp.Text;
                current_local.Cidade = txtCidadeLocalUp.Text;
                //Limpa o comboBox e popula ele denovo com a exclusão feita
                cmbSetLocal.Text = "";
                cmbSetLocal.Items.Clear();
                //Atualiza o listbox  depois de Excluir com o foreach
                foreach (Local local_item in locais)
                {
                    //Configurar-se a string que será colocada na seleção do combolist
                    string texto = local_item.Id + " - " + local_item.Nome;
                    //local_item é a isntancia de cada objeto percorrendo o list global de Locais
                    cmbSetLocal.Items.Add(texto);
                }

                //Atualiza o combobox do cadastro de viagem
                cmbLinkLocal.Items.Clear();

                foreach (Local local_item in locais)
                {
                    //Configurar-se a string que será colocada na seleção do combolist
                    string texto = local_item.Nome;
                    //local_item é a isntancia de cada objeto percorrendo o list global de Locais
                    cmbLinkLocal.Items.Add(texto);
                }
                //Limpa os dados dos form
                txtNomeLocalUp.Text = "";
                txtCidadeLocalUp.Text = "";
            }
        }




        private void btnDellLocal_Click(object sender, EventArgs e)
        {
            try
            {
                locais.Remove(current_local);
                txtNomeLocalUp.Text = "";
                txtCidadeLocalUp.Text = "";
                cmbSetLocal.Text = "";
            }
            catch
            {
                MessageBox.Show("Local não selecioando");
            }

            //Limpa o comboBox e popula ele denovo com a exclusão feita
            cmbSetLocal.Items.Clear();

            //Atualiza o listbox  depois de Excluir com o foreach
            foreach (Local local_item in locais)
            {
                //Configurar-se a string que será colocada na seleção do combolist
                string texto = local_item.Id + " - " + local_item.Nome;
                //local_item é a isntancia de cada objeto percorrendo o list global de Locais
                cmbSetLocal.Items.Add(texto);
            }

            //Atualiza o combobox do cadastro de viagem
            cmbLinkLocal.Items.Clear();

            foreach (Local local_item in locais)
            {
                //Configurar-se a string que será colocada na seleção do combolist
                string texto = local_item.Nome;
                //local_item é a isntancia de cada objeto percorrendo o list global de Locais
                cmbLinkLocal.Items.Add(texto);
            }
        }

        //Quando selecionar algum item dos locais cadastrado
        private void cmbSetLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            //atribuindo o valor do item selecionado
            string itemSelecionado = cmbSetLocal.SelectedItem.ToString();
            //Quebra a string separado em duas partes atribuindo a um array de string
            string[] string_quebrada = itemSelecionado.Split('-');
            //A primeira parte tem o Id
            int id = Convert.ToInt32(string_quebrada[0]);
            //percorre o array para achar a referencia do objeto atraves do id
            foreach (Local local_item in locais)
            {
                if (local_item.Id == id)
                {
                    txtNomeLocalUp.Text = local_item.Nome;
                    txtCidadeLocalUp.Text = local_item.Cidade;
                    current_local = local_item;
                    break;
                }
            }

        }


        #endregion

        //ABA de Consulta\\
        #region Aba de Consulta
        private void Top5Viajantes()
        {
            lb5Viajantes.Items.Clear();
            Viajante aux = null;
            for (int i = 0; i < viajantes.Count; i++)
            {
                for (int j = 0; j < viajantes.Count - 1; j++)
                {
                    if (viajantes[j].viagens.Count > viajantes[j + 1].viagens.Count)
                    {
                        aux = viajantes[j];
                        viajantes[j] = viajantes[j + 1];
                        viajantes[j + 1] = aux;
                    }
                }
            }
            int a = 0;
            foreach (Viajante viajantes_item in viajantes)
            {
                if (a <= 5)
                {
                    lb5Viajantes.Items.Add(viajantes_item.Id + " - " + viajantes_item.Nome);
                }
                else {
                    break;
                }
                a++;
            }



        }

        private void LocalMaisViajados()
        {
            List<Top> tops = new List<Top>();
            lb5Viajados.Items.Clear();
            List<Top> lista = new List<Top>();
            foreach (Local locais_items in locais)
            {
                int count = 0;
                foreach (Viagem viagens_item in viagens)
                {
                    if (viagens_item.local == locais_items)
                    {
                        count = count + 1;
                    }
                }
                Top temp = new Top();
                temp.local = locais_items;
                temp.count = count;
                tops.Add(temp);


            }
            int a = 0;
            tops.Sort((x, y) => x.count);
            foreach (Top top_item in tops)
            {
                if (a <= 5)
                {
                    lb5Viajados.Items.Add(top_item.local.Nome + " Qtd de Viagem: " + top_item.count);
                }
                else
                {
                    break;
                }
                a++;
            }

        }
       

        private void MaiorClassificacao()
        {
            List<Top> tops = new List<Top>();
            lblClass.Items.Clear();
            List<Top> lista = new List<Top>();
            foreach (Local locais_items in locais)
            {
                int count = 0;
                foreach (Viagem viagens_item in viagens)
                {
                    if (viagens_item.local == locais_items)
                    {
                        count = count + viagens_item.classificacao;
                    }
                }
                Top temp = new Top();
                temp.local = locais_items;
                temp.count = count;
                tops.Add(temp);
                Ordernar(tops);

            }
            int a = 0;
            Ordernar(tops);
            foreach (Top top_item in tops)
            {
                if (a <= 5)
                {
                    lblClass.Items.Add(top_item.local.Nome + " Classificação Total: " + top_item.count);
                }
                else
                {
                    break;
                }
                a++;
            }
        }
        public List<Top> Ordernar(List<Top> objeto)
        {
            Top aux = null;
            for (int i = 0; i < objeto.Count; i++)
            {
                for (int j = 0; j < objeto.Count - 1; j++)
                {
                    if (objeto[i].count > objeto[j + 1].count)
                    {
                        aux = objeto[j];
                        objeto[j] = objeto[j + 1];
                        objeto[j + 1] = aux;
                    }
                }
            }
            return objeto;

        }

        private void txtIdViajantes_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtIdViagem_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtIdLocal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            Top5Viajantes();
            LocalMaisViajados();
            MaiorClassificacao();
        }
    }


    #endregion


}





//

