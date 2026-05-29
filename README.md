# Chess Game in C#

## About the Project
This is a chess game project developed in C# as part of a study on object-oriented programming (OOP). The main goal was to apply advanced OOP concepts such as encapsulation, inheritance, polymorphism, and abstraction, while implementing a functional and interactive game system — including official chess rules and special moves.

The game follows a layered architecture, organizing the board logic, pieces, and user interaction in a modular and reusable way. It runs in the console and allows two players to alternate turns, enforcing game rules, detecting check and checkmate, and supporting special moves such as Castling, En Passant, and Pawn Promotion.

## Tech Stack
- Language: C#
- Paradigm: Object-Oriented Programming (OOP)
- Data Structures: Matrices, Sets
- Exception Handling

## Implemented Features

### Board Structure
- `Position` class to represent board coordinates.
- Matrix-based representation of board squares.
- `Board` class with methods to manage pieces and positions.

### Chess Pieces
- Individual classes for each piece (King, Rook, Bishop, Knight, Queen, Pawn).
- Inheritance to reuse code and define shared behavior across all pieces.
- Movement rules defined for each piece type.

### Game Mechanics
- Turn system allowing players to alternate moves.
- Validation to prevent illegal moves.
- Piece capture control.
- Check and checkmate detection logic.

### Special Moves
- **Kingside and Queenside Castling:** Special movement involving the king and rook.
- **En Passant:** Special pawn capture.
- **Promotion:** Pawn transformation upon reaching the last rank.

## How to Play
1. Clone the repository to your machine:
```bash
git clone https://github.com/marias1lva/projetoXadrez.git
```
2. Open the project in a C#-compatible IDE (e.g. Visual Studio, VS Code).
3. Build and run the program.
4. Follow the terminal instructions to play.

## Example Gameplay

The game starts by displaying the board and prompting the player to choose a piece to move — entering the origin position, pressing Enter, then entering the destination.

```bash
Initial board position:
8 |R N B Q K B N R
7 |P P P P P P P P
6 |- - - - - - - -
5 |- - - - - - - -
4 |- - - - - - - -
3 |- - - - - - - -
2 |P P P P P P P P
1 |R N B Q K B N R
   ---------------
   a b c d e f g h

Captured pieces:
White: []
Black: []

Turn: 1
Waiting for move: White

Origin: e2
(Enter)
Destination: e4


Board after player input:

8 |R N B Q K B N R
7 |P P P P P P P P
6 |- - - - - - - -
5 |- - - - - - - -
4 |- - - - P - - -
3 |- - - - - - - -
2 |P P P P - P P P
1 |R N B Q K B N R
   ---------------
   a b c d e f g h

Captured pieces:
White: []
Black: []

Turn: 2
Waiting for move: Black

Origin:
(Enter)
Destination:
```

### Player Input
- Enter origin and destination coordinates when prompted.
- **Supported notation:** a1–h8
- **Example:**
```bash
Origin: e2
Destination: e4  (moves a pawn from e2 to e4)
```

## Error Messages
- **"There is already a piece in that position!"** → The player attempted to move to a square occupied by a friendly piece. You cannot capture your own pieces.
- **"Invalid position!"** → The coordinate is out of bounds (e.g. i5 or a0) or does not follow the correct format.
- **"You cannot put yourself in check!"** → The move would leave the player's own king in check, which is forbidden by chess rules.
- **"There is no piece at the chosen origin position!"** → The player attempted to move from an empty square, likely due to a typo in the coordinate.
- **"The chosen piece does not belong to you!"** → The player attempted to move the opponent's piece. You may only move your own pieces.
- **"There are no possible moves for the chosen piece!"** → The selected piece has no legal moves available due to game rules or blockage by other pieces.
- **"Invalid destination position!"** → The selected piece cannot move to the given destination according to chess movement rules.
- **"There is no (White or Black) King on the board!"** → The game detected that one of the kings is missing from the board, indicating a state error or a bug in the capture logic.

## Contributing
Contributions are welcome! To get started:
- Fork the repository.
- Create a new branch with your changes.
- Open a pull request for review.

----

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

## Exemplo de Execução

- O jogo inicia exibindo o tabuleiro e solicitando que o jogador escolha a peça que deseja mover, informando a posição de origem, clicando enter e informando o destino.
```bash
Posição inicial do tabuleiro:             
8 |T C B D R B C T
7 |P P P P P P P P
6 |- - - - - - - -
5 |- - - - - - - -
4 |- - - - - - - -
3 |- - - - - - - -
2 |P P P P P P P P
1 |T C B D R B C T
   ---------------
   a b c d e f g h

Peças capturadas:
Brancas: []
Pretas: []

Turno: 1
Aguardando jogada: Branca

Origem: e2
(Enter)
Destino: e4


Tabuleiro após entrada do usuário:

8 |T C B D R B C T
7 |P P P P P P P P
6 |- - - - - - - -
5 |- - - - - - - -
4 |- - - - P - - -
3 |- - - - - - - -
2 |P P P P - P P P
1 |T C B D R B C T
   ---------------
   a b c d e f g h

Peças capturadas:
Brancas: []
Pretas: []

Turno: 2
Aguardando jogada: Preta

Origem: 
(Enter)
Destino: 
```
- ### Comandos do usuário:
  - Para fazer um movimento, digite as coordenadas de origem e destino, primeiro é exibida a `Origem` no terminal, e em seguida o `Destino`;
  - **Notação suportada**: a1-h8;
  - **Exemplo**:
```bash
   Origem: e2
   Destino: e4 (move um peão da casa e2 para a casa e4)
```

## Mensagens de Erro
- **"Já existe uma peça nessa posição!"** → O jogador tentou mover uma peça para uma casa já ocupada por outra peça da mesma cor. No xadrez, você não pode capturar suas próprias peças.
- **"Posição inválida!"** → A coordenada inserida está fora do tabuleiro (exemplo: i5 ou a0), ou não segue o formato correto.
- **"Você não pode se colocar em xeque!"** → A jogada feita deixaria o próprio rei em xeque, o que é proibido pelas regras do xadrez.
- **"Não existe peça na posição de origem escolhida!"** → O jogador tentou mover uma peça a partir de uma casa vazia. Isso pode ocorrer ao digitar uma coordenada errada.
- **"A peça de origem escolhida não é sua!"** → O jogador tentou mover uma peça do adversário. No xadrez, você só pode jogar com suas próprias peças.
- **"Não há movimentos possíveis para a peça de origem escolhida!"** → O jogador escolheu uma peça que, devido às regras do jogo ou ao bloqueio por outras peças, não tem movimentos legais disponíveis.
- **"Posição de destino inválida!"** → A peça selecionada não pode se mover para a posição de destino informada, pois não obedece às regras de movimentação do xadrez.
- **"Não tem Rei da cor (branca ou preta) no tabuleiro!"** → O jogo detectou que um dos reis não está no tabuleiro, o que pode indicar um erro no estado da partida ou um problema na implementação da captura do rei.


## Contribuições
Sinta-se à vontade para contribuir com melhorias, correções e novas funcionalidades. Para isso:
- Faça um fork do repositório;
- Crie uma nova branch com suas alterações;
- Envie um pull request para análise.
