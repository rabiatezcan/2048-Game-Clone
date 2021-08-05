using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection
{
    Left,
    Right,
    Up,
    Down
}

public class Cell : MonoBehaviour
{
    public Cell right;
    public Cell left;
    public Cell up;
    public Cell down;
    
    public Fill fill;
    
    private void OnEnable()
    {
        GameController.slide += OnSlide;
    }

    private void OnDisable()
    {
        GameController.slide -= OnSlide;
    }
    
    private Cell ControlDirection(MoveDirection direction)
    {
        if (direction == MoveDirection.Down)
            return up;
        else if (direction == MoveDirection.Left)
            return right;
        else if (direction == MoveDirection.Right)
            return left;
        else
            return down;
    }

    private void OnSlide(MoveDirection direction)
    {
        CellCheck(); 
        Cell currentCell = this;
        if (direction == MoveDirection.Up)
        {
            if (up != null)
                return;
        }

        if (direction == MoveDirection.Right)
        {
            if (right != null)
                return;
        }

        if (direction == MoveDirection.Down)
        {
            if (down != null)
                return;
        }

        if (direction == MoveDirection.Left)
        {
            if (left != null)
                return;
        }
        
        Slide(currentCell, direction);
    }

    private void Slide(Cell currentCell, MoveDirection direction)
    {
        
        if (currentCell.ControlDirection(direction) == null)
            return;

        Cell nextCell = currentCell.ControlDirection(direction);
        while (nextCell.ControlDirection(direction) != null && nextCell.fill == null)
        {
            nextCell = nextCell.ControlDirection(direction);
        }

        if (nextCell.fill != null)
        {
            if (currentCell.fill != null)
            {
                if (currentCell.fill.value == nextCell.fill.value)
                {
                    nextCell.fill.Double();
                    nextCell.fill.transform.parent = currentCell.transform;
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if (currentCell.ControlDirection(direction).fill != nextCell.fill)
                {
                    nextCell.fill.transform.parent = currentCell.ControlDirection(direction).transform;
                    currentCell.ControlDirection(direction).fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
            else
            {
                nextCell.fill.transform.parent = currentCell.transform;
                currentCell.fill = nextCell.fill;
                nextCell.fill = null;
                Slide(currentCell, direction);
            }
        }

        if (currentCell.ControlDirection(direction) == null)
            return;
        Slide(currentCell.ControlDirection(direction), direction);
    }
    void CellCheck()
    {
        if (fill == null)
        {
            return;
        }

        if (up != null)
        {
            if (up.fill == null)
                return;
            if (up.fill.value == fill.value)
                return;
        }

        if (down != null)
        {
            if (down.fill == null)
                return;
            if (down.fill.value == fill.value)
                return;
        }

        if (right != null)
        {
            if (right.fill == null)
                return;
            if (right.fill.value == fill.value)
                return;
        }

        if (left != null)
        {
            if (left.fill == null)
                return;
            if (left.fill.value == fill.value)
                return;
        }
    }
}