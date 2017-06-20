using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	public Transform verificaChao;
	public Transform verificaParede;

	public float velocidade;
	public float raioValidaChao;
	public float raioValidaParede;

	public LayerMask solido;

	private Rigidbody2D rb;
	private Transform tr;
	private Animator an;

	private bool estaNoChao;
	private bool estaNaParede;
	private bool viradoParaDireita;

	// Use this for initialization
	void Awake () {

		rb = GetComponent<Rigidbody2D> ();
		tr = GetComponent<Transform> ();
		an = GetComponent<Animator> ();

		viradoParaDireita = true;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		EnemyMovements ();
	}

	void EnemyMovements()
	{
		estaNoChao = Physics2D.OverlapCircle (verificaChao.position, raioValidaChao, solido);
		estaNaParede = Physics2D.OverlapCircle (verificaParede.position, raioValidaParede, solido);

		if (!estaNoChao || estaNaParede) {
			Flip ();
		}

		if (estaNoChao)
			rb.velocity = new Vector2 (velocidade, rb.velocity.y);
	}

	void Flip()
	{
		velocidade = -velocidade;

		viradoParaDireita = !viradoParaDireita;

		tr.localScale = new Vector2 (-tr.localScale.x, tr.localScale.y);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;

		Gizmos.DrawWireSphere (verificaChao.position, raioValidaChao);
		Gizmos.DrawWireSphere (verificaParede.position, raioValidaParede);

	}
}
