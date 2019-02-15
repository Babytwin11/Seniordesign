using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace S3
{
    public class Deal_Damage : MonoBehaviour

    {
        //  public int tee = GetComponent<player_controler>().TeamIde;


        //dimples

        void OnCollisionEnter(Collision collision)
        {


            // collision for dimples
            GameObject hit = collision.gameObject;
            Debug.Log(hit);
            Collider coli = hit.GetComponent<Collider>();
            playerhealth health = hit.GetComponent<playerhealth>();
            Debug.Log(health);
            // collision for cyborg
            GameObject hit2 = collision.gameObject;
            Player_health health2 = hit.GetComponent<Player_health>();
            Team mytm = this.GetComponent<Team>();
            Team tm = hit.GetComponent<Team>();
            //int help = GetComponent<player_controler>().TeamIde;
            // help = tee;
            //|| tm.TeamId != mytm.TeamId
            TeamRed red = GetComponent<TeamRed>();
            TeamGreen green = GetComponent<TeamGreen>();
            try
            {
                Debug.Log("haha1");
                bool yesred = hit.GetComponent<TeamRed>().red;


                //  bool yesgreen= hit.GetComponent<TeamGreen>().green;
                if (yesred == true)

                {

                    if (coli.CompareTag("Dimples") || hit.CompareTag("Dimples"))
                    {
                        try
                        {

                            TeamRed red4 = GetComponent<TeamRed>();
                            Debug.Log(red4);
                            if (yesred == true && red4 == null)
                            {
                                if (health != null)
                                {
                                    Debug.Log("green1");
                                    Debug.Log("12");
                                    health.TakeDamage(10);
                                }
                                if (health2 != null)
                                {
                                    health2.DeductHealth(10);

                                }
                                ////TeamRed green4 = GetComponent<TeamRed>();

                                Debug.Log("green7779");
                                return;
                            }
                            else
                            {
                                //TeamRed green4 = GetComponent<TeamRed>();

                                Debug.Log("green5389");
                                return;
                            }
                        }
                        catch (NullReferenceException)
                        {
                            if (health != null)
                            {
                                Debug.Log("green1");
                                Debug.Log("12");
                                health.TakeDamage(10);
                            }
                            if (health2 != null)
                            {
                                health2.DeductHealth(10);

                            }

                            Debug.Log("green89");
                        }


                        return;


                    }

                }
            }
            catch (NullReferenceException)
            {
                if (health != null)
                {
                    try
                    {
                        bool yesgreen = hit.GetComponent<TeamGreen>().green;
                        TeamRed myred = GetComponent<TeamRed>();
                        if (yesgreen == true && myred == false)
                        {
                            Debug.Log("green0");
                            return;
                        }
                        else
                        {
                            if (health != null)
                            {
                                Debug.Log("green1");
                                Debug.Log("12");
                                health.TakeDamage(10);
                            }
                            if (health2 != null)
                            {
                                health2.DeductHealth(10);

                            }
                        }
                    }
                    catch (NullReferenceException)
                    {
                        if (health != null)
                        {
                            Debug.Log("green2");
                            Debug.Log("dse 13 555");
                            health.TakeDamage(10);
                        }
                        if (health2 != null)
                        {
                            health2.DeductHealth(10);

                        }
                    }

                }
                if (health2 != null)
                {
                    health2.DeductHealth(10);

                }
            }






            if (tm == null || tm.TeamId != mytm.TeamId)
            {
                Debug.Log("haha2");
                Debug.Log("are you coming here4");
                Debug.Log("dse are here");
                Debug.Log("dse are 444");
                health.TakeDamage(10);
                if (health != null)
                {
                    Debug.Log("dse are 555");
                    health.TakeDamage(10);
                }
                if (health2 != null)
                {
                    health2.DeductHealth(10);
                }

                Destroy(gameObject);
            }


        }
    }
}