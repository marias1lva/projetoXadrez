using tabuleiro;

namespace xadrez {
    class Rei : Peca {
        private PartidaDeXadrez partida;
        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor) {
            this.partida = partida;
        }
        public override string ToString() {
            return "R";
        }

        private bool podeMover(Posicao pos) {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor; // O Rei só pode se mover se a posição estiver vazia ou a peça for de cor diferente
        }

        private bool testeTorreParaRoque(Posicao pos) {
            Peca p = tab.peca(pos);
            return p != null && p is Torre && p.cor == cor && p.qteMovimentos == 0;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            // Acima
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos)) { // Se a posição é válida e se pode mover
                mat[pos.linha, pos.coluna] = true; // Verifica se a posição acima do Rei está livre ou com peça adversária, se sim, ele pode se mover para lá
            }

            // Nordeste
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { 
                mat[pos.linha, pos.coluna] = true; 
            }

            // Direita
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { 
                mat[pos.linha, pos.coluna] = true; 
            }

            // Sudeste
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { 
                mat[pos.linha, pos.coluna] = true; 
            }

            // Abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos)) { 
                mat[pos.linha, pos.coluna] = true; 
            }

            // Sudoeste
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { 
                mat[pos.linha, pos.coluna] = true; 
            }

            // Esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { 
                mat[pos.linha, pos.coluna] = true;
            }

            // Noroeste
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { 
                mat[pos.linha, pos.coluna] = true; 
            }

            // #JogadaEspecial Roque Pequeno
            if (qteMovimentos == 0 && !partida.xeque) {
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna + 3);
                if (testeTorreParaRoque(posT1)) {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                    if (tab.peca(p1) == null && tab.peca(p2) == null) {
                        mat[posicao.linha, posicao.coluna + 2] = true;
                    }

                }
            }

            // #JogadaEspecial Roque Grande
            if (qteMovimentos == 0 && !partida.xeque) {
                Posicao posT2 = new Posicao(posicao.linha, posicao.coluna - 4);
                if (testeTorreParaRoque(posT2)) {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);
                    if (tab.peca(p1) == null && tab.peca(p2) == null && tab.peca(p3) == null) {
                        mat[posicao.linha, posicao.coluna - 2] = true;
                    }

                }
            }

            return mat;
        }
    }
}

// O Roque Pequeno é quando o Rei anda duas casas para a direita e a Torre anda duas para a esquerda, só pode ocorrer quando o Rei e a Torre não se moveram ainda,
// as casas entre eles estiverem livres e o Rei não estiver em xeque.

// O Roque Grande é quando o Rei anda duas casas para a esquerda e a Torre anda três para a direita, só pode ocorrer quando o Rei e a Torre não se moveram ainda,
// as casas entre eles estiverem livres e o Rei não estiver em xeque.