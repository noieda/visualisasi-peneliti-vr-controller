namespace VRTK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class VRTK_PressedObject : VRTK_InteractableObject
    {
        public GameObject Manager;
        public GameObject DashboardButtonChild1;
        bool DBA = false; // Dashboard Button Active
        bool PBA = false; // Peneliti Button Active
        bool ABA = false; // Abjad Button Active
        bool FBA = false; // Fakultas Button Active
        bool TBA = false; // Table Button Active

        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);
            //Manager.GetComponent<EventHandler>().buttonPressed(name);
            if (GetComponent<NodeVariable>() != null)
            {
                if (CompareTag("ListPenelitiAbjad"))
                {
                    Debug.Log("lpa " + name);
                    ///Manager.GetComponent<EventHandler>().buttonPressed(tag, name);
                    Manager.GetComponent<Manager>().getPenelitiInisialITS(name);

                }
                else if(CompareTag("ListPenelitiInisial"))
                {
                    string kode = GetComponent<NodeVariable>().kode_peneliti;
                    Debug.Log(kode + " <- NodeVariable");
                    Manager.GetComponent<Manager>().getDetailPenelitiITS(kode);
                    //Manager.GetComponent<Manager>().getDetailPenelitiITS
                }
                else if(CompareTag("ListPenelitiFakultas"))
                {
                    string kode = GetComponent<NodeVariable>().kode_peneliti;
                    Debug.Log(kode + " <- fakultaspeneliti");
                    Manager.GetComponent<Manager>().getPenelitiDepartemenITS(kode);
                }
                else if(CompareTag("ListPenelitiDepartemen"))
                {

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
                    DashboardButtonChild1.SetActive(DBA);
                    Manager.GetComponent<EventHandler>().Dashboard();
                    Manager.GetComponent<Manager>().DashboardToggle();
                }
                else if (name == "PenelitiButton")
                {
                    Debug.Log("peneliti button");
                    PBA = !PBA;
                    DashboardButtonChild1.SetActive(PBA);
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
                else if (name == "SizeUpButton")
                {
                    Debug.Log("size up button");
                    //Manager.GetComponent<Manager>().sizeCoef += 0.002f;
                    Manager.GetComponent<Manager>().resizeNode(0.2f);
                }
                else if (name == "SizeDownButton")
                {
                    Debug.Log("size down button");
                    //Manager.GetComponent<Manager>().sizeCoef -= 0.002f;
                    Manager.GetComponent<Manager>().resizeNode(-0.2f);
                }
                else if (name == "NavigationButton")
                {
                    Debug.Log("navigation button");
                    TBA = !TBA;
                    Manager.GetComponent<Manager>().TableButton.SetActive(TBA);
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
                if (CompareTag("ListPenelitiAbjad"))
                {
                    Debug.Log("touch a-> " + name);
                    ///Manager.GetComponent<EventHandler>().buttonPressed(tag, name);
                    Manager.GetComponent<EventHandler>().peekPenelitiAbjadITS(gameObject);

                }
                else if (CompareTag("ListPenelitiInisial"))
                {
                    Debug.Log("touch b-> " + name);
                    ///Manager.GetComponent<EventHandler>().buttonPressed(tag, name);
                    Manager.GetComponent<EventHandler>().peekPenelitiAbjadITS(gameObject);

                }
                else if (CompareTag("ListPenelitiFakultas"))
                {
                    Debug.Log("touch c-> " + name);
                    Manager.GetComponent<EventHandler>().peekPenelitiAbjadITS(gameObject);

                }
            }

        }

        public override void StopTouching(VRTK_InteractTouch usingObject)
        {
            base.StartTouching(usingObject);
            Debug.Log(name + " <- finished touched");
            GameObject peekDelete = GameObject.Find("peekPeneliti");
            if (peekDelete != null)
            {
                Destroy(peekDelete);
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