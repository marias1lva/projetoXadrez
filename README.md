# Jogo de Xadrez em C#

## Sobre o Projeto
Este é um projeto de um jogo de xadrez desenvolvido em C# como parte de um estudo sobre programação orientada a objetos (POO). O objetivo principal do projeto foi aplicar conceitos avançados de POO, como encapsulamento, herança, polimorfismo e abstração, além de implementar um sistema de jogo funcional e interativo, incluindo regras oficiais do xadrez e jogadas especiais.

O jogo foi estruturado seguindo um padrão de camadas, organizando a lógica do tabuleiro, das peças e da interação com o usuário de maneira modular e reutilizável. A aplicação roda no console e permite que dois jogadores alternem jogadas, respeitando as regras do jogo, identificando xeque e xequemate, além de oferecer suporte a jogadas especiais, como Roque, En Passant e Promoção de Peão.

## Tecnologias Utilizadas
- Linguagem: C#
- Paradigma: Programação Orientada a Objetos (POO)
- Estruturas de Dados: Matrizes, Conjuntos
- Tratamento de Exceções

## Recursos Implementados
### Estrutura do Tabuleiro:
- Criada a classe Posicao para representar posições no tabuleiro;
- Implementado o conceito de matriz para representar as casas do tabuleiro;
- Criada a classe Tabuleiro com métodos para gerenciar peças e posições.
### Peças do Xadrez:
- Criadas as classes específicas para cada peça (Rei, Torre, Bispo, Cavalo, Dama, Peão);
- Implementada a herança para reutilizar código e definir comportamentos genéricos para todas as peças;
- Definição dos movimentos possíveis para cada peça.
### Mecânica do Jogo:
- Implementado o sistema de turnos, permitindo alternância entre os jogadores;
- Validações para impedir jogadas inválidas;
- Controle de captura de peças;
- Implementação da lógica de xeque e xequemate.
### Jogadas Especiais:
- Roque Pequeno e Roque Grande: Movimentação especial do rei e da torre;
- En Passant: Captura especial do peão;
- Promoção: Transformação do peão ao atingir a última fileira.

## Como jogar
1. Para jogar o jogo clone o repositório em seu computador com o comando abaixo:
```bash
git clone https://github.com/marias1lva/projetoXadrez.git
```
2. Abra o projeto em um ambiente de desenvolvimento compatível com C# (ex: Visual Studio, VS Code);
3. Compile e execute o programa;
4. Siga as instruções no terminal para jogar.

Exemplo de Uso

O jogo inicia exibindo o tabuleiro e solicitando que o jogador escolha a peça que deseja mover, informando a posição de origem e destino.
```bash
Turno 1 - Jogador: Branco
Origem: e2
Destino: e4
```

## Contribuições
Sinta-se à vontade para contribuir com melhorias, correções e novas funcionalidades. Para isso:
- Faça um fork do repositório;
- Crie uma nova branch com suas alterações;
- Envie um pull request para análise.
