﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	public Transform verificaChao;
	public Transform verificaParede;

	public float velocidade;
	public float forcaPulo;
	public float raioValidaChao;
	public float raioValidaParede;

	public LayerMask solido;



	private Rigidbody2D rb;
	private Transform tr;
	private Animator an;

	private bool estaAndando;
	private bool estaNoChao;
	private bool estaNaParede;
	private bool estaVivo;
	private bool viradoParaDireita;
	private bool estaPulando;

	private float axis;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
		tr = GetComponent<Transform> ();
		an = GetComponent<Animator> ();

		estaVivo = true;
		viradoParaDireita = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		estaNoChao = Physics2D.OverlapCircle (verificaChao.transform, raioValidaChao, solido);
		estaNaParede = Physics2D.OverlapCircle (verificaParede.transform, raioValidaParede, solido);

		if (estaNoChao)
			estaPulando = false;

		axis = Input.GetAxisRaw ("Horizontal");

		estaAndando = Mathf.Abs (axis) > 0;

		if (axis > 0f && !viradoParaDireita)
			Flip ();
		else if (axis < 0f && viradoParaDireita)
			Flip ();

		if (Input.GetButtonDown ("Jump") && estaNoChao)
			estaPulando = true;
	}

	void FixedUpdate()
	{

		if (estaPulando)
			rb.AddForce (tr.up * forcaPulo);

		if (estaAndando && !estaNaParede)
		{
			if (viradoParaDireita)
				rb.velocity = new Vector2 (velocidade, rb.velocity.y);
			else
				rb.velocity = new Vector2 (-velocidade, rb.velocity.y);
		}
	}

	void Flip()
	{
		viradoParaDireita = !viradoParaDireita;

		tr.localScale = new Vector2 (-tr.localScale.x, tr.localScale.y);
	}
}