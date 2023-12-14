using System;

class Nodo
{
    public int Valor;
    public Nodo Izquierdo, Derecho;

    public Nodo(int valor)
    {
        Valor = valor;
        Izquierdo = Derecho = null;
    }
}

class ArbolBinario
{
    private Nodo raiz;

    public ArbolBinario()
    {
        raiz = null;
    }

    public void Insertar(int valor)
    {
        raiz = InsertarRecursivo(raiz, valor);
    }

    private Nodo InsertarRecursivo(Nodo raizActual, int valor)
    {
        if (raizActual == null)
        {
            return new Nodo(valor);
        }

        if (valor < raizActual.Valor)
        {
            raizActual.Izquierdo = InsertarRecursivo(raizActual.Izquierdo, valor);
        }
        else if (valor > raizActual.Valor)
        {
            raizActual.Derecho = InsertarRecursivo(raizActual.Derecho, valor);
        }

        return raizActual;
    }

    public void Eliminar(int valor)
    {
        raiz = EliminarRecursivo(raiz, valor);
    }

    private Nodo EliminarRecursivo(Nodo raizActual, int valor)
    {
        if (raizActual == null)
        {
            return raizActual;
        }

        if (valor < raizActual.Valor)
        {
            raizActual.Izquierdo = EliminarRecursivo(raizActual.Izquierdo, valor);
        }
        else if (valor > raizActual.Valor)
        {
            raizActual.Derecho = EliminarRecursivo(raizActual.Derecho, valor);
        }
        else
        {
            // NODO CON UN SOLO HIJO O SIN HIJOD
            if (raizActual.Izquierdo == null)
            {
                return raizActual.Derecho;
            }
            else if (raizActual.Derecho == null)
            {
                return raizActual.Izquierdo;
            }

            // NODO CON DOS HIJOS: ENCONTRAR EL SUCESOR INORDEN (EL MENOR VALOR EN EL SÚBARBOL DERECHO)
            raizActual.Valor = MinimoValor(raizActual.Derecho);

            // ELIMINAR EL SUCESOR INORDEN
            raizActual.Derecho = EliminarRecursivo(raizActual.Derecho, raizActual.Valor);
        }

        return raizActual;
    }

    private int MinimoValor(Nodo raizActual)
    {
        int minValor = raizActual.Valor;
        while (raizActual.Izquierdo != null)
        {
            minValor = raizActual.Izquierdo.Valor;
            raizActual = raizActual.Izquierdo;
        }
        return minValor;
    }

    public bool Buscar(int valor)
    {
        return BuscarRecursivo(raiz, valor);
    }

    private bool BuscarRecursivo(Nodo raizActual, int valor)
    {
        if (raizActual == null)
        {
            return false;
        }

        if (valor == raizActual.Valor)
        {
            return true;
        }

        return valor < raizActual.Valor ?
            BuscarRecursivo(raizActual.Izquierdo, valor) :
            BuscarRecursivo(raizActual.Derecho, valor);
    }

    // Método para realizar un recorrido inorden del árbol
    public void RecorridoInorden()
    {
        RecorridoInorden(raiz);
        Console.WriteLine();
    }

    private void RecorridoInorden(Nodo raizActual)
    {
        if (raizActual != null)
        {
            RecorridoInorden(raizActual.Izquierdo);
            Console.Write(raizActual.Valor + " ");
            RecorridoInorden(raizActual.Derecho);
        }
    }
}

class Program
{
    static void Main()
    {
        ArbolBinario arbol = new ArbolBinario();

        // INSERTAR ELEMENTOS EN EL ÁRBOL
        arbol.Insertar(50);
        arbol.Insertar(30);
        arbol.Insertar(70);
        arbol.Insertar(20);
        arbol.Insertar(40);
        arbol.Insertar(60);
        arbol.Insertar(80);

        Console.WriteLine("Recorrido Inorden del Árbol:");
        arbol.RecorridoInorden();

        // BUSCAR UN ELEMENTO EN EL ÁRBOL
        int elementoABuscar = 40;
        Console.WriteLine($"¿El elemento {elementoABuscar} está en el árbol?: {arbol.Buscar(elementoABuscar)}");

        // ELIMINAR UN ELEMENTO DEL ÁRBOL
        int elementoAEliminar = 30;
        arbol.Eliminar(elementoAEliminar);
        Console.WriteLine($"Recorrido Inorden después de eliminar {elementoAEliminar}:");
        arbol.RecorridoInorden();

        Console.ReadLine();
    }
}