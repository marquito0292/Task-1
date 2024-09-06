using ApiTask1.Service.Interface;

namespace ApiTask1.Service
{
    public class CalculadoraService: ICalculadoraService
    {

        //Creamos nuestro metodo Suma
        public string Sumar(float a, float b)
        {
            try
            {
                float resultado = a + b;
                return $"La suma es: {resultado}";
            }
            catch (Exception ex)
            {
                // Capturamos cualquier excepción inesperada y la devolvemos como mensaje de error
                return $"Error al realizar la suma: {ex.Message}";
            }
        }

        public string Resta(float a, float b)
        {
            try
            {
                float resultado = a - b;
                return $"La resta es: {resultado}";

            }catch (Exception ex)
            {
                // Capturamos cualquier excepción inesperada y la devolvemos como mensaje de error
                return $"Error al realizar la resta: {ex.Message}";

            }
        }
    }
}
