﻿using System;
using System.Collections.Generic;
using System.Text;

namespace uninfe
{
    public class DropDownListClass
    {
        public DropDownListClass(string nome, int valor)
        {
            Nome = nome;
            Valor = valor;
        }       

        string _nome;
        int _valor;
        
        public int Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }
        
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
    }
}