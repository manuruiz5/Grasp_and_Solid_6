//-------------------------------------------------------------------------
// <copyright file="Recipe.cs" company="Universidad Católica del Uruguay">
// Copyright (c) Programación II. Derechos reservados.
// </copyright>
//-------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;

namespace Full_GRASP_And_SOLID
{
    public class Recipe : IRecipeContent // Modificado por DIP
    {
        // Cambiado por OCP
        private IList<BaseStep> steps = new List<BaseStep>();

        public Product FinalProduct { get; set; }
        public bool Cooked {get;private set;}=false;//Propiedad agregada para saber si la receta esta cocida

        // Agregado por Creator
        public void AddStep(Product input, double quantity, Equipment equipment, int time)
        {
            Step step = new Step(input, quantity, equipment, time);
            this.steps.Add(step);
        }

        // Agregado por OCP y Creator
        public void AddStep(string description, int time)
        {
            WaitStep step = new WaitStep(description, time);
            this.steps.Add(step);
        }

        public void RemoveStep(BaseStep step)
        {
            this.steps.Remove(step);
        }
        // Agregado por SRP
        public string GetTextToPrint()
        {
            string result = $"Receta de {this.FinalProduct.Description}:\n";
            foreach (BaseStep step in this.steps)
            {
                result = result + step.GetTextToPrint() + "\n";
            }

            // Agregado por Expert
            result = result + $"Costo de producción: {this.GetProductionCost()}";

            return result;
        }
        // Agregado por Expert
        public double GetProductionCost()
        {
            double result = 0;

            foreach (BaseStep step in this.steps)
            {
                result = result + step.GetStepCost();
            }

            return result;
        }

        //Agregado mediante el principio Expert:
        public int GetCookTime()//Método que calcula el tiempo de cocción de la receta
        {
            int totalTime=0;
            foreach (BaseStep step in this.steps) //Recorre cada paso de la receta
            {
                totalTime+=step.Time; //Suma el tiempo de cada paso a el total
            }
            return totalTime;
        }
        //Principios SRP y Expert:
        public void Cook() //Método para la cocción de la receta
        {
            CountdownTimer timer=new CountdownTimer(); //Crea un nuevo temporizador de cuenta regresiva
            RecipeAdapter adapter=new RecipeAdapter(this); //Crea un adaptador de receta
            timer.Register(GetCookTime(),adapter); //Usa el register de countdowntimer 
            Cooked=true; //Declara la receta como cocida
        }
        //Agregado por SRP y Expert:
        public void CookedStatus(bool cooked) //Método para cambiar el estado de cocción
        {
            this.Cooked=cooked; //Establece el estado de la receta
        }
    }
}