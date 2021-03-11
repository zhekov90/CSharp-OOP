﻿using System;
using System.Collections.Generic;
using System.Text;


public class Box
{
    private double length;
    private double width;
    private double height;

    public Box(double length, double width, double height)
    {
        this.Length = length;
        this.Width = width;
        this.Height = height;
    }
    public double Length
    {
        get
        {
            return this.length;
        }
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Length cannot be zero or negative.");
            }
           
                this.length = value;
            
        }
    }
    public double Width
    {
        get
        {
            return this.width;
        }
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Width cannot be zero or negative.");
            }
           
                this.width = value;
            
            
        }
    }
    public double Height
    {
        get
        {
            return this.height;
        }
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Height cannot be zero or negative.");
            }
            
               this.height = value;
            
        }
    }

    public double SurfaceArea(double length, double width, double height)
    {
        var calculateSurfaceArea
            = (2 * length * width)
            + (2 * length * height)
            + (2 * width * height);
        return calculateSurfaceArea;
    }

    public double LateralSurfaceArea(double length, double width, double height)
    {
        var calculateLateralArea
            = (2 * length * height)
            + (2 * width * height);
        return calculateLateralArea;
    }

    public double Volume(double length, double width, double height)
    {
        var calculateVolume
            = length * width * height;
        return calculateVolume;
    }
}

