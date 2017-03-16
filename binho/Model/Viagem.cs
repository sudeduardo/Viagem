using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace binho.Model
{
    public class Viagem
    {
        private int Id;
        private string Descricao;
        private int Classificacao;
        private string Foto;
        public Local local = new Local();

    

        public int iD
        {
            get
            {
                return Id;
            }

            set
            {
                Id = value;
            }
        }

        public string descricao
        {
            get
            {
                return Descricao;
            }

            set
            {
                Descricao = value;
            }
        }

        public int classificacao
        {
            get
            {
                return Classificacao;
            }

            set
            {
                Classificacao = value;
            }
        }

        public string foto
        {
            get
            {
                return Foto;
            }

            set
            {
                Foto = value;
            }
        }

      

       
    }
}
