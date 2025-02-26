namespace tabuleiro {
    class Peca {
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

    }
}

// { get; protected set } = acessado por outras classes (get) e alterado somente por ela mesma e pelas subclasses dela 