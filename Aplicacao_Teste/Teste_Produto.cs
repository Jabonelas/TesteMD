using Aplicacao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Teste_Pratico.Infra;

namespace Aplicacao_Teste
{
    [TestClass]
    public class Teste_Produto
    {
        [TestMethod]
        public void InserirProdutoTest()
        {
            //Preparacao
            Aplicacao_produto aplicacao = new Aplicacao_produto();

            //Acao
            bool resultado = aplicacao.InseririProduto();

            //Resultado
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void BuscarTodosProdutosAtivosTest()
        {
            //Preparacao
            Aplicacao_produto aplicacao = new Aplicacao_produto();

            //Acao
            var resultado = aplicacao.BuscarTodosProdutos();

            //Resultado
            Assert.IsTrue(resultado.Count > 0);
        }
    }
}