using tabuleiro;

namespace xadrez {
    class Peao : Peca {

        private PartidaDeXadrez partida;
        public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor) {
            this.partida = partida;
        }
        public override string ToString() {
            return "P";
        }
        
        private bool existeInimigo(Posicao pos) {
            Peca p = tab.peca(pos);
            return p != null && p.cor != cor;
        }
        private bool livre(Posicao pos) {
            return tab.peca(pos) == null;
        }
        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            if (cor == Cor.Branca) {
                pos.definirValores(posicao.linha - 1, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 2, posicao.coluna);
                Posicao p2 = new Posicao(posicao.linha - 1, posicao.coluna);
                if (tab.posicaoValida(p2) && livre(p2) && tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                // #JogadaEspecial En Passant
                if (posicao.linha == 3) {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant) { // Verifica se na casa da esquerda existe um peão e se ele está vulnerável a tomar um En Passant
                        mat[esquerda.linha - 1, esquerda.coluna] = true; // Se sim, a posição da matriz é marcada como posição possível para o peão mexer
                    }
                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant) { // Verifica se na casa da direita existe um peão e se ele está vulnerável a tomar um En Passant
                        mat[direita.linha - 1, direita.coluna] = true; // Se sim, a posição da matriz é marcada como posição possível para o peão mexer
                    }
                }

            } else {
                pos.definirValores(posicao.linha + 1, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha + 2, posicao.coluna);
                Posicao p2 = new Posicao(posicao.linha + 1, posicao.coluna);
                if (tab.posicaoValida(p2) && livre(p2) && tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                // #JogadaEspecial En Passant
                if (posicao.linha == 4) {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant) { // Verifica se na casa da esquerda existe um peão e se ele está vulnerável a tomar um En Passant
                        mat[esquerda.linha + 1, esquerda.coluna] = true; // Se sim, a posição da matriz é marcada como posição possível para o peão mexer
                    }
                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant) { // Verifica se na casa da direita existe um peão e se ele está vulnerável a tomar um En Passant
                        mat[direita.linha + 1, direita.coluna] = true; // Se sim, a posição da matriz é marcada como posição possível para o peão mexer
                    }
                }
            }

            return mat;
        }
    }
}

// En Passant: é uma regra especial do xadrez que permite que um peão capture outro peão de uma maneira única.
// Se um peão adversário avançar duas casas de sua posição inicial em um único movimento, e parar ao lado do seu peão, você pode capturá-lo como se ele
// tivesse andado apenas uma casa.
// A captura só pode ser feita no turno seguinte ao movimento do peão adversário. Se você não capturar o peão adversário imediatamente, perde a oportunidade.


// Promoção: é uma regra especial do xadrez que permite que um peão que alcançou a última linha do tabuleiro seja promovido a qualquer outra peça (nesse jogo
// o peão automaticamente vira uma dama, o jogador não tem chance de escolha).