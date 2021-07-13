var gameboard = { size:{ x:3, y:3 } };
var player = { position:{ x:1, y:1 } };
var item = { position:{ x:1, y:1 } };

var score = 0;

// Перерисовка поля (при инициализации и изменении состояния игры)
function RedrawBoard() {
	var board = document.getElementById("game-board");
	
	for(x=0;x<gameboard.size.x; x++)
	{
		for(y=0; y<gameboard.size.y; y++)
		{
			var cell = board.rows[y].cells[x];
			cell.classList.remove("player-cell");
			cell.classList.remove("item-cell");
			
			if(x==player.position.x && y==player.position.y)
				cell.classList.add("player-cell");
				
			if(x==item.position.x && y==item.position.y)
				cell.classList.add("item-cell");
		}
	}
}

function Move(dir)
{
	switch(dir)
	{
		case "N": if(player.position.y != 0) player.position.y--; break;
		case "E": if(player.position.x < gameboard.size.x-1) player.position.x++; break;
		case "W": if(player.position.x != 0) player.position.x--; break;
		case "S": if(player.position.y < gameboard.size.y-1) player.position.y++; break;
	}
	
	CheckItemCollision();
	
	RedrawBoard();
}

function PlaceItem()
{
	var item_x = player.position.x;
	var item_y = player.position.y;
	
	while(item_x == player.position.x && item_y == player.position.y)
	{
		item_x = Math.floor(Math.random()*gameboard.size.x);
		item_y = Math.floor(Math.random()*gameboard.size.y);
	}
	
	item.position.x = item_x;
	item.position.y = item_y;
}

function CheckItemCollision()
{
	// Если игрок и вещь находятся на одной позиции
	if(player.position.x == item.position.x &&
	   player.position.y == item.position.y)
	{
		// Увеличиваем счётчик очков
		score++;
		// Перемещаем вещь в другое место
		PlaceItem();
		
		// Обновить текст на табло очков (score-block)
		RedrawScore();
	}
}

function Reset()
{
	score = 0;
	player.position.x=1;
	player.position.y=1;
	PlaceItem();
	
	RedrawBoard();
	RedrawScore();
	Hide(false);
}

function RedrawScore()
{
	var scoreblock = document.getElementById("score-block");
	if(score >= 5) 
	{
		scoreblock.innerText = "Victory!";
		Hide(true);
	}
	else scoreblock.innerText = "Score: "+score;
	
	
}

function Hide(toggle)
{
	var gamectrl = document.getElementById("game-controls");
	if(toggle) gamectrl.style.display = "none";
	else gamectrl.style.display = "block";
}