namespace tabuleiro {
    abstract class Peca { // Se a classe tem pelo menos um método abstrato, ela deve ser abstrata
        public Posicao posicao { get; set; }

        public Cor cor { get; protected set; }

        public int qteMovimentos { get; protected set; }

        public Tabuleiro tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor) {
            this.posicao = null;
            this.tab = tab;
            this.cor = cor;
            this.qteMovimentos = 0;
        }

        public void incrementarQteMovimentos() {
            qteMovimentos++;
        }

        public void decrementarQteMovimentos() {
            qteMovimentos--;
        }

        public bool existeMovimentosPossiveis() { // Verifica se na matriz de movimentos possíveis existe pelo menos um valor verdadeiro (movimento possível)
            bool[,] mat = movimentosPossiveis();
            for (int i = 0; i < tab.linhas; i++) {
                for (int j = 0; j < tab.colunas; j++) {
                    if (mat[i, j]) {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool podeMoverPara(Posicao pos) { // Verifica se a peça pode mover para uma determinada posição, se essa posição é um dos movimentos possíveis dela
            return movimentosPossiveis()[pos.linha, pos.coluna]; 
        }

        public abstract bool[,] movimentosPossiveis();

    }
}

// { get; protected set } = acessado por outras classes (get) e alterado somente por ela mesma e pelas subclasses dela 