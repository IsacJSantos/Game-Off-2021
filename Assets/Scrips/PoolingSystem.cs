using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
//https://jogoscomcafe.wordpress.com/2020/02/27/tutorial-object-pooling-unity/
{
    //Informa��es que ser�o preenchidas no Inspector
    [SerializeField] private Pool[] pool;

    //Dicion�rios que guardam as informa��es do pool
    private Dictionary<string, Queue<GameObject>> dicionarioPool = new Dictionary<string, Queue<GameObject>>();
    private Dictionary<string, GameObject> dicionarioPrefab = new Dictionary<string, GameObject>();

    public static PoolingSystem Instancia;

    private void Awake()
    {
        Instancia = this;
    }

    private void Start()
    {
        InicializaDicionario();
    }

    private void InicializaDicionario()
    {
        //Passa por cada pool criado pelo usu�rio para adicionar a chave e o Queue ao dicion�rio
        for (int i = 0; i < pool.Length; i++)
        {
            //Cria a fila
            Queue<GameObject> poolObjetos = new Queue<GameObject>();

            //Cria o primeiro objeto
            GameObject objeto = Instantiate(pool[i].Prefab);
            objeto.SetActive(false);
            poolObjetos.Enqueue(objeto);

            //Insere entrada nos dicion�rios
            dicionarioPool.Add(pool[i].Key, poolObjetos);
            dicionarioPrefab.Add(pool[i].Key, pool[i].Prefab);
        }
    }

    public GameObject GetObjeto(string key, Vector2 posicao, Quaternion rotacao)
    {
        //Checa se existe essa tag no dicion�rio
        if (!dicionarioPool.ContainsKey(key))
        {
            Debug.Log("N�o cont�m " + key);
            return null;
        }

        //Verifica se existe um objeto no pool ou se ele est� ativo
        if (dicionarioPool[key].Peek().activeSelf)
        {
            //Instancia um novo
            GameObject objetoPooled = Instantiate(dicionarioPrefab[key]);
            //Insere novo objeto no dicion�rio
            dicionarioPool[key].Enqueue(objetoPooled);
            //Retorna objeto
            return objetoPooled;
        }
        else
        {
            //Pega da fila
            GameObject objetoPooled = dicionarioPool[key].Dequeue();
            //Coloca o objeto na posi��o e rota��o corretas
            objetoPooled.transform.position = posicao;
            objetoPooled.transform.rotation = rotacao;
            //Ativa objeto
            objetoPooled.SetActive(true);
            //Coloca de volta
            dicionarioPool[key].Enqueue(objetoPooled);
            //Retorna objeto
            return objetoPooled;
        }
    }

    //Classe que permite definir as vari�veis no Inspector
    [System.Serializable]
    public class Pool
    {
        public string Key;
        public GameObject Prefab;
    }
}