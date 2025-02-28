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
        public Peca vulneravelEnPassant { get; private set; } // Quando alguém mexer um peão a primeira vez, duas casas, a partida vai armazenar essa peça dizendo que ela está vulnerável a tomar um En Passant no próximo turno

        public PartidaDeXadrez() {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            vulneravelEnPassant = null;
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

            // #JogadaEspecial Roque Pequeno
            if(p is Rei && destino.coluna == origem.coluna + 2) {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQteMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            // #JogadaEspecial Roque Grande
            if (p is Rei && destino.coluna == origem.coluna - 2) {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQteMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            // #JogadaEspecial En Passant
            if (p is Peao) {
                if (origem.coluna != destino.coluna && pecaCapturada == null) {
                    Posicao posP;
                    if (p.cor == Cor.Branca) { // Se a peça que foi movida é um Peão branco, isso significa que a posição do peão capturado é a de baixo
                        posP = new Posicao(destino.linha + 1, destino.coluna);
                    } else { // Se a peça que foi movida é um Peão preto, isso significa que a posição do peão capturado é a de cima
                        posP = new Posicao(destino.linha - 1, destino.coluna);
                    }
                    pecaCapturada = tab.retirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
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

            // #JogadaEspecial Roque Pequeno
            if(p is Rei && destino.coluna == origem.coluna + 2) {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQteMovimentos();
                tab.colocarPeca(T, origemT);
            }

            // #JogadaEspecial Roque Grande
            if (p is Rei && destino.coluna == origem.coluna - 2) {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQteMovimentos();
                tab.colocarPeca(T, origemT);
            }

            // #JogadaEspecial En Passant
            if (p is Peao) {
                if (origem.coluna != destino.coluna && pecaCapturada == vulneravelEnPassant) {
                    Peca peao = tab.retirarPeca(destino); // Tira o peão que foi movido
                    Posicao posP; 
                    if (p.cor == Cor.Branca) { // Se a peça que foi movida é um Peão branco, a posição que o peão preto tem que ser colocado é a linha 3 e a mesma coluna
                        posP = new Posicao(3, destino.coluna);
                    }
                    else { // Se a peça que foi movida é um Peão preto, a posição que o peão branco tem que ser colocado é a linha 4 e a mesma coluna
                        posP = new Posicao(4, destino.coluna);
                    }
                    tab.colocarPeca(peao, posP);
                }
            }
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

            if (testeXequemate(adversaria(jogadorAtual))) {
                terminada = true;
            } else {
                turno++;
                mudaJogador();
            }

            Peca p = tab.peca(destino);

            // #JogadaEspecial En Passant
            if (p is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2)) { // Testa se a peça que foi movida é um Peão e se ela andou duas linhas a mais ou a menos (o movimento foi de duas casas) 
                    vulneravelEnPassant = p;
            } else {
                vulneravelEnPassant = null;
            }

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
            if (!tab.peca(origem).movimentoPossivel(destino)) { // Se a peça de origem escolhida não pode mover para a posição de destino escolhida
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

        public bool testeXequemate(Cor cor) {
            if (!estaEmXeque(cor)) { // Se a cor passada como parâmetro não está em xeque
                return false; // Não é xeque-mate
            }
            foreach (Peca x in pecasEmJogo(cor)) {
                bool[,] mat = x.movimentosPossiveis(); // Pega a matriz de movimentos possíveis da peça x
                for (int i = 0; i < tab.linhas; i++) {
                    for (int j = 0; j < tab.colunas; j++) {
                        if (mat[i, j]) { // Se a matriz de movimentos possíveis da peça x tem um movimento possível
                            Posicao origem = x.posicao; // Pega a posição da peça x
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino); // Executa o movimento da peça x para a posição i, j
                            bool testeXeque = estaEmXeque(cor); // Testa se a cor passada como parâmetro está em xeque
                            desfazMovimento(origem, destino, pecaCapturada); // Desfaz o movimento da peça x para a posição i, j
                            if (!testeXeque) { // Se a cor passada como parâmetro não está em xeque
                                return false; // Não é xeque-mate
                            }
                        }
                    }
                }
            }
            return true; // É xeque-mate
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca) { 
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao()); // Dado uma coluna, uma linha e uma peça, eu vou no tabuleiro da partida e coloco
            pecas.Add(peca); // Adiciono a peça no conjunto de peças
        }

        private void colocarPecas() {
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca));

            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta));
        }
    }
}
