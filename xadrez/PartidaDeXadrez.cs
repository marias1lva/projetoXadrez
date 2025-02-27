using System;
using System.Collections.Generic;
using tabuleiro;

namespace xadrez {
    class PartidaDeXadrez {

        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }

        public PartidaDeXadrez() {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executaMovimento(Posicao origem, Posicao destino) {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null) {
                capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQteMovimentos();
            if (pecaCapturada != null) {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);
        }

        public void realizaJogada(Posicao origem, Posicao destino) {
            Peca pecaCapturada = executaMovimento(origem, destino);

            if(estaEmXeque(jogadorAtual)) {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if (estaEmXeque(adversaria(jogadorAtual))) {
                xeque = true;
            } else {
                xeque = false;
            }

            turno++;
            mudaJogador();
        }

        public void validarPosicaoDeOrigem(Posicao pos) {
            if (tab.peca(pos) == null) { // Se não existe peça na posição de origem escolhida
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (jogadorAtual != tab.peca(pos).cor) { // Se a peça de origem escolhida não é do jogador atual (jogador "branco" escolhe peça "preta" ou vice-versa)
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis()) { // Se a peça de origem escolhida não tem movimentos possíveis
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino) {
            if (!tab.peca(origem).podeMoverPara(destino)) { // Se a peça de origem escolhida não pode mover para a posição de destino escolhida
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void mudaJogador() {
            if (jogadorAtual == Cor.Branca) {
                jogadorAtual = Cor.Preta;
            } else {
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas) { // Percorre todas as peças x no conjunto de capturadas
                if (x.cor == cor) { // Se a cor da peça x for igual a cor passada como parâmetro, essa peça x é adicionada ao conjunto aux
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas) { // Percorre todas as peças x no conjunto de peças
                if (x.cor == cor) { // Se a cor da peça x for igual a cor passada como parâmetro, essa peça x é adicionada ao conjunto aux
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor)); // Remove do conjunto aux as peças capturadas da cor passada como parâmetro
            return aux;
        }

        private Cor adversaria(Cor cor) {
            if (cor == Cor.Branca) { // Se a cor passada como parâmetro for branca, a cor adversária é preta
                return Cor.Preta;
            } else { // Se a cor passada como parâmetro for preta, a cor adversária é branca
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor) {
            foreach (Peca x in pecasEmJogo(cor)) { // Percorre todas as peças x no conjunto de peças em jogo da cor passada como parâmetro
                if (x is Rei) { // Se a peça x for um Rei, ela é retornada
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Cor cor) {
            Peca R = rei(cor); // Pega o Rei da cor passada como parâmetro
            if (R == null) { // Se não existe Rei da cor passada como parâmetro
                throw new TabuleiroException("Não tem Rei da cor " + cor + " no tabuleiro!");
            }
            foreach (Peca x in pecasEmJogo(adversaria(cor))) { // Percorre todas as peças x no conjunto de peças em jogo da cor adversária
                bool[,] mat = x.movimentosPossiveis(); // Pega a matriz de movimentos possíveis da peça x
                if (mat[R.posicao.linha, R.posicao.coluna]) { // Se a matriz de movimentos possíveis da peça x tem a posição do Rei da cor passada como parâmetro
                    return true; // O Rei da cor passada como parâmetro está em xeque
                }
            }
            return false; // O Rei da cor passada como parâmetro não está em xeque
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca) { 
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao()); // Dado uma coluna, uma linha e uma peça, eu vou no tabuleiro da partida e coloco
            pecas.Add(peca); // Adiciono a peça no conjunto de peças
        }

        private void colocarPecas() {
            colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));

            colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));

        }
    }
}
