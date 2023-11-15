using System;
using System.Collections.Generic;

namespace Full_GRASP_And_SOLID
{
    /*Cumple con el principio SRP, ya que tiene una unica responsabilidad que es adaptar la 
    lógica de TimeOut al método CookedStatus de la clase REcipe. Cumple con el principio DIP 
    esto es debido a que depende de una abstracción (TimerClient) en vez de una implementación 
    concreta. Cumple también con el patrón Expert ya que tiene la información necesaria para 
    cambiar el estado de cocción de la receta */
    public class RecipeAdapter:TimerClient
    {
        private Recipe recipe; //Receta que se va a adaptar
        public RecipeAdapter(Recipe recipe)
        //Constructor de la clase recipe adapter  que recibe la receta 
        {
            this.recipe=recipe;
        }
        public void TimeOut() //Método para cuando el tiempo es agotado en el temporizador 
        {
            this.recipe.CookedStatus(true); //Cambia el estado de cocción de la receta
        }
    }
}