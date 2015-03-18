using System;
using GXPEngine;
using System.Drawing;
using GXPEngine.Core;

namespace GXPEngine
{
	public class TileWorld : Canvas
	{
		protected Canvas _gridView;
		protected bool[,] _tileData;

		protected int _columns;
		protected int _rows;
		protected int _tileSize;

		public TileWorld (int pColumns, int pRows, int pTileSize):base(pColumns * pTileSize, pRows * pTileSize)
		{
			//store base values
			_columns = pColumns;
			_rows = pRows;
			_tileSize = pTileSize;
		
			//create underlying data structure
			_tileData = new bool[_columns,_rows];

			//generate the data in our world
			initializeWorld ();
			//draw our world
			drawWorld ();

			//draw a grid over our world
			_gridView = new Canvas (width+1, height+1);
			AddChild (_gridView);
			drawGrid ();
		}

		public int columns { get { return _columns; } }
		public int rows { get { return _rows; } }
		public int tileSize { get { return _tileSize; } }

		public void SetWalkable (int pColumn, int pRow, bool pWalkable) {
			_tileData [pColumn,pRow] = pWalkable;
			drawTile (pColumn, pRow);
		}

		public void SetWalkable (IntVec2 pPosition, bool pWalkable) {
			_tileData [pPosition.x,pPosition.y] = pWalkable;
			drawTile (pPosition.x, pPosition.y);
		}

		public bool IsWalkable (int pColumn, int pRow) {
			return pColumn >= 0 && pColumn < _columns &&  pRow >=0 && pRow < _rows && _tileData[pColumn, pRow];
		}

		public bool IsWalkable (IntVec2 pPosition) {
			return IsWalkable (pPosition.x, pPosition.y);
		}

		virtual protected void initializeWorld() {
			for (int column = 0; column < _columns; column++) {
				for (int row = 0; row < _rows; row++) {
					_tileData [column, row] = true;
				}
			}
		}

		virtual protected void drawWorld() {
			for (int column = 0; column < _columns; column++) {
				for (int row = 0; row < _rows; row++) {
					drawTile (column, row);
				}
			}
		}

		virtual protected void drawTile (int pColumn, int pRow) {
			graphics.FillRectangle (
				IsWalkable(pColumn, pRow) ? Brushes.White : Brushes.Black, 	//pick color based on walkable or not
				pColumn * _tileSize, 										//x
				pRow * _tileSize, 											//y
				_tileSize, 													//width
				_tileSize													//height
			);
		}

		virtual protected void drawGrid() {
			for (int column = 0; column <= _columns; column++) {
				_gridView.graphics.DrawLine (Pens.Black, column * _tileSize, 0, column * _tileSize, height);
			}

			for (int row = 0; row <= _rows; row++) {
				_gridView.graphics.DrawLine (Pens.Black, 0, row * _tileSize, width, row * _tileSize);
			}
		}

		///////////////////////////////////// AGENT ADDITIONS ////////////////////////////////////////////

		public void AddAgent (AbstractAgent pAgent) {
			pAgent.SetContext (this);
			AddChild (pAgent);
		}


	}
}

