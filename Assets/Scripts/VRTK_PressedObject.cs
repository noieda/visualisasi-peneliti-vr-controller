namespace VRTK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class VRTK_PressedObject : VRTK_InteractableObject
    {
        public GameObject Manager;
        public GameObject ButtonDisplay;
        bool DBA = false; // Dashboard Button Active
        bool PBA = false; // Peneliti Button Active
        bool ABA = false; // Abjad Button Active
        bool FBA = false; // Fakultas Button Active
        bool GBA = false; // Gelar Button Active
        bool LBA = false; // Laboratorium Button Active
        bool TBA = false; // Table Button Active
        bool OBA = false; // Option Button Active

        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);
            //Manager.GetComponent<EventHandler>().buttonPressed(name);
            if (GetComponent<NodeVariable>() != null)
            {
                if (CompareTag("ListPenelitiAbjad"))
                {
                    //Debug.Log("lpa " + name);
                    //Manager.GetComponent<Manager>().getPenelitiInisialITS(name);

                    string kode = GetComponent<NodeVariable>().kode_peneliti;
                    Debug.Log(kode + " <- NodeVariable");
                    Manager.GetComponent<Manager>().getPenelitiInisialITS(kode);
                }
                else if(CompareTag("ListPenelitiInisial"))
                {
                    string kode = GetComponent<NodeVariable>().kode_peneliti;
                    Debug.Log(kode + " <- NodeVariable");
                    Manager.GetComponent<Manager>().OpenResearcherDetail();
                    Manager.GetComponent<Manager>().getDetailPenelitiITS(kode);
                }
                
                else if(CompareTag("ListPenelitiFakultas"))
                {
                    string kode = GetComponent<NodeVariable>().kode_peneliti;
                    Debug.Log(kode + " <- fakultaspeneliti");
                    Manager.GetComponent<Manager>().getPenelitiDepartemenITS(kode);
                }
                else if(CompareTag("ListPenelitiDepartemen"))
                {
                    string kode = GetComponent<NodeVariable>().kode_peneliti;
                    Debug.Log(kode + " <- NodeVariable");
                    Manager.GetComponent<Manager>().getPenelitiDepartemenDetailITS(kode);
                }
                else if(CompareTag("ListPenelitiDepartemenDetail"))
                {
                    string kode = GetComponent<NodeVariable>().kode_peneliti;
                    Debug.Log(kode + " <- NodeVariable");
                    Manager.GetComponent<Manager>().OpenResearcherDetail();
                    Manager.GetComponent<Manager>().getDetailPenelitiITS(kode);
                }
                
                else if(CompareTag("ListGelar"))
                {
                    string kode = GetComponent<NodeVariable>().kode_peneliti;
                    Debug.Log(kode + " <- NodeVariable");
                    Manager.GetComponent<Manager>().getGelarPenelitiDetail(kode);
                }
                else if (CompareTag("ListGelarDetail"))
                {
                    string kode = GetComponent<NodeVariable>().kode_peneliti;
                    Debug.Log(kode + " <- NodeVariable");
                    Manager.GetComponent<Manager>().OpenResearcherDetail();
                    Manager.GetComponent<Manager>().getDetailPenelitiITS(kode);
                }


                else if(CompareTag("ListPublikasiFakultas"))
                {
                    string kode = GetComponent<NodeVariable>().kode_peneliti;
                    Debug.Log(kode + " <- NodeVariable");
                    Manager.GetComponent<Manager>().getPublikasiKataKunci(kode);
                }
                else if (CompareTag("ListPublikasiKataKunci"))
                {
                    string kode = GetComponent<NodeVariable>().kode_peneliti;
                    string nama = name;
                    //Debug.Log(kode + " <- NodeVariable");
                    Manager.GetComponent<Manager>().getKataKunciPeneliti(kode, nama);
                }
                else if (CompareTag("ListKataKunciPeneliti"))
                {
                    string kode = GetComponent<NodeVariable>().kode_peneliti;
                    string nama = name;
                    //Debug.Log(kode + " <- NodeVariable");
                    Manager.GetComponent<Manager>().OpenResearcherDetail();
                    Manager.GetComponent<Manager>().getDetailPenelitiITS(kode);
                }


                
                else
                {
                    Debug.Log("a");
                }
            }
            else
            {
                if (name == "DashboardButton")
                {
                    Debug.Log("dashboard button");
                    DBA = !DBA;
                    //ButtonDisplay.SetActive(DBA);
                    Manager.GetComponent<EventHandler>().Dashboard();
                    Manager.GetComponent<Manager>().DashboardToggle();
                }
                else if (name == "PenelitiButton")
                {
                    Debug.Log("peneliti button");
                    PBA = !PBA;
                    ButtonDisplay.SetActive(PBA);
                    Manager.GetComponent<Manager>().PenelitianToggle();
                }
                else if (name == "AbjadButton")
                {
                    Debug.Log("abjad button");
                    ABA = !ABA;
                    Manager.GetComponent<Manager>().getPenelitiAbjadITS();
                }
                else if (name == "FakultasButton")
                {
                    Debug.Log("fakultas button");
                    FBA = !FBA;
                    Manager.GetComponent<Manager>().getPenelitiFakultasITS();
                }
                else if (name == "GelarButton") 
                {
                    Debug.Log("gelar button");
                    GBA = !GBA;
                    Manager.GetComponent<Manager>().getGelarPenelitiITS();
                }
                else if (name == "PublikasiButton")
                {
                    Debug.Log("PublikasiButton");
                    LBA = !LBA;
                    Manager.GetComponent<Manager>().getPublikasiFakultas();
                }
                else if (name == "NavigationButton")
                {
                    Debug.Log("navigation button");
                    TBA = !TBA;
                    Manager.GetComponent<Manager>().TableButton.SetActive(TBA);
                }
                
                
                else if (name == "OptionButton")
                {
                    Debug.Log("option button");
                    OBA = !OBA;
                    Manager.GetComponent<Manager>().OptionToggle();
                }
                else if (name == "BiggerNodeButton")
                {
                    Debug.Log("size up button");
                    Manager.GetComponent<Manager>().resizeNode(0.1f);
                }
                else if (name == "SmallerNodeButton")
                {
                    Debug.Log("size down button");
                    Manager.GetComponent<Manager>().resizeNode(-0.1f);
                }
                else if (name == "FasterNodeAnimationButton")
                {
                    Debug.Log("faster node animation");
                    Manager.GetComponent<Manager>().changeSpeedNode(0.05f);
                }
                else if (name == "SlowerNodeAnimationButton")
                {
                    Debug.Log("slower node animation");
                    Manager.GetComponent<Manager>().changeSpeedNode(-0.05f);
                }
                else if (name == "xMinusButton")
                {
                    Debug.Log("xminus button");
                    Manager.GetComponent<Manager>().movePositionNode("x", -0.5f);
                }
                else if (name == "xPlusButton")
                {
                    Debug.Log("xminus button");
                    Manager.GetComponent<Manager>().movePositionNode("x", 0.5f);
                }
                else if (name == "yMinusButton")
                {
                    Debug.Log("yminus button");

                    Manager.GetComponent<Manager>().movePositionNode("y", -0.5f);
                }
                else if (name == "yPlusButton")
                {
                    Debug.Log("yplus button");

                    Manager.GetComponent<Manager>().movePositionNode("y", 0.5f);
                }
                else if (name == "zMinusButton")
                {
                    Debug.Log("zminus button");

                    Manager.GetComponent<Manager>().movePositionNode("z", -0.5f);
                }
                else if (name == "zPlusButton")
                {
                    Debug.Log("zplus button");

                    Manager.GetComponent<Manager>().movePositionNode("z", 0.5f);
                }
                else if (name == "LessTransparentNodeButton")
                {
                    Debug.Log("less transparent button");

                    Manager.GetComponent<Manager>().transparentNode(0);
                }
                else if (name == "NormalTransparentNodeButton")
                {
                    Debug.Log("normal transparent button");

                    Manager.GetComponent<Manager>().transparentNode(1);

                }
                else if (name == "MoreTransparentNodeButton")
                {
                    Debug.Log("more transparent button");

                    Manager.GetComponent<Manager>().transparentNode(2);
                }

                else if (name == "TutupDetailPeneliti")
                {
                    Debug.Log("tutup detail button");
                    Manager.GetComponent<Manager>().CloseResearcherDetail();
                }


                else
                {
                    Debug.Log(name);
                }
            }
            

            //Debug.Log("startUsing - " + name);
        }

        public override void StartTouching(VRTK_InteractTouch usingObject)
        {
            base.StartTouching(usingObject);
            Debug.Log(name + " <- touched");
            
            if (gameObject.GetComponent<NodeVariable>() != null)
            {
                
                if (CompareTag("ListPublikasiFakultas") || CompareTag("ListPublikasiKataKunci") || CompareTag("ListPenelitiInisial") || CompareTag("ListPenelitiDepartemenDetail") || CompareTag("ListGelarDetail") || CompareTag("ListKataKunciPeneliti")) 
                {
                    Debug.Log("touch vrtk");
                    Manager.GetComponent<EventHandler>().peekNodePeneliti(gameObject, "publications");
                }
                else
                {
                    Debug.Log("touch vrtk");
                    Manager.GetComponent<EventHandler>().peekNodePeneliti(gameObject);
                }
                
            }

        }

        public override void StopTouching(VRTK_InteractTouch usingObject)
        {
            base.StopTouching(usingObject);
            Debug.Log(name + " <- finished touched");
            GameObject peekDelete = Manager.GetComponent<EventHandler>().peekPeneliti;
            
            if (peekDelete.activeSelf != false)
            {
                //Destroy(peekDelete);
                peekDelete.SetActive(false);
            }
            GameObject usingObject2 = (usingObject != null ? usingObject.gameObject : null);

            if (usingObject2 != null && touchingObjects.Remove(usingObject2))
            {
                ResetUseState(usingObject2);
                OnInteractableObjectUntouched(SetInteractableObjectEvent(usingObject2));
            }
        }

        public override void Grabbed(VRTK_InteractGrab usingObject)
        {
            base.Grabbed(usingObject);
            Debug.Log("Grabbed");
        }

        // Start is called before the first frame update
        public void Start()
        {
            Manager = GameObject.Find("Manager");
            //Debug.Log("start");
        }

        // Update is called once per frame
        protected override void Update()
        {
            //base.Update();
            //Debug.Log("update");
        }
    }

}