using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour {

	public enum MonsterState{idle, trace, attack, die};
	public MonsterState monsterState = MonsterState.idle;

	private Transform monsterTr;
	private Transform playerTr;
	private NavMeshAgent nvAgent;
	// Use this for initialization

	public float tracedist = 10.0f;
	public float attackdist = 2.0f;
	// 각종 사정 거리

	private bool isDie = false;

	void Start () {
		monsterTr = this.gameObject.GetComponent<Transform> ();
		// find 류는 처리 속도가 느리므로 업데이트에서는 쓰이지 않는다 awake나 start에서 쓰길 
		playerTr = GameObject.FindWithTag ("Player").GetComponent<Transform> ();

		nvAgent = this.gameObject.GetComponent<NavMeshAgent> ();

		// not good code
		// nvAgent.destination = playerTr.position;


		StartCoroutine (this.CheckMonsterState()); 
	}

	IEnumerator CheckMonsterState(){
		while (!isDie) {

			yield return new WaitForSeconds (0.2f);

			float dist = Vector3.Distance (playerTr.position, monsterTr.position);

			if (dist <= attackdist) {
				monsterState = MonsterState.attack;
			} 
			else if (dist <= tracedist) {
				monsterState = MonsterState.trace;
			}
			else {
				monsterState = MonsterState.idle;
			}
		}
	}

	IEnumerator MonsterAction(){
		while (!isDie) {
			switch (monsterState) {
			case MonsterState.idle:
				nvAgent.Stop ();
				break;

			case MonsterState.trace:
				nvAgent.destination = playerTr.position;
				nvAgent.Resume ();
				break;

			case MonsterState.attack:
				break;
			}
			yield return null;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
