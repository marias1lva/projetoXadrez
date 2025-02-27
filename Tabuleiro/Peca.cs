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

        public abstract bool[,] movimentosPossiveis();

        public void incrementarQteMovimentos() {
            qteMovimentos++;
        } 

    }
}

// { get; protected set } = acessado por outras classes (get) e alterado somente por ela mesma e pelas subclasses dela 