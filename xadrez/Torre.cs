using tabuleiro;

namespace xadrez {
    internal class Torre : Peca {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor) {
        }
        public override string ToString() {
            return "T";
        }

        private bool podeMover(Posicao pos) {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor; // O Rei só pode se mover se a posição estiver vazia ou a peça for de cor diferente
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            // Acima
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            while(tab.posicaoValida(pos) && podeMover(pos)) { // Enquando tiver casa livre ou peça adversária, a Torre pode se mover
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) { // Se a posição tiver uma peça adversária, a Torre não pode passar por ela (tem que parar na casa)
                    break;
                }
                pos.linha = pos.linha - 1;
            }

            // Abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            while (tab.posicaoValida(pos) && podeMover(pos)) { 
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) { 
                    break;
                }
                pos.linha = pos.linha + 1;
            }

            // Direita
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }
                pos.coluna = pos.coluna + 1;
            }

            // Esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }
                pos.coluna = pos.coluna - 1;
            }

            return mat;
        }
    }
}
