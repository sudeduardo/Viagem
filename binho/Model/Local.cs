using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace binho.Model
{
    public class Local
    {
        private int id;
        private string nome;
        private string cidade;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                if (value > 0) { 
                id = value;
                }else
                {
                    id = 0;
                }
            }
        }

        public string Nome
        {
            get
            {
                return nome;
            }

            set
            {
                nome = value;
            }
        }

        public string Cidade
        {
            get
            {
                return cidade;
            }

            set
            {
                cidade = value;
            }
        }
    }
}
