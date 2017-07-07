using UnityEngine.UI;

namespace VRTK.Examples
{
    using UnityEngine;

    public class VRTK_ControllerPointerEvents_ListenerExample : MonoBehaviour
    {
		public SelectMode sel;

        private void Start()
        {
			sel = GameObject.Find("Mode").GetComponent<SelectMode>();

            if (GetComponent<VRTK_SimplePointer>() == null)
            {
                Debug.LogError("VRTK_ControllerPointerEvents_ListenerExample is required to be attached to a Controller that has the VRTK_SimplePointer script attached to it");
                return;
            }

            //Setup controller event listeners
            GetComponent<VRTK_SimplePointer>().DestinationMarkerEnter += new DestinationMarkerEventHandler(DoPointerIn);
            GetComponent<VRTK_SimplePointer>().DestinationMarkerExit += new DestinationMarkerEventHandler(DoPointerOut);
            GetComponent<VRTK_SimplePointer>().DestinationMarkerSet += new DestinationMarkerEventHandler(DoPointerDestinationSet);
        }

        private void DebugLogger(uint index, string action, Transform target, RaycastHit raycastHit, float distance, Vector3 tipPosition)
        {
            string targetName = (target ? target.name : "<NO VALID TARGET>");
            string colliderName = (raycastHit.collider ? raycastHit.collider.name : "<NO VALID COLLIDER>");
            Debug.Log("Controller on index '" + index + "' is " + action + " at a distance of " + distance + " on object named [" + targetName + "] on the collider named [" + colliderName + "] - the pointer tip position is/was: " + tipPosition);
        }

        private void DoPointerIn(object sender, DestinationMarkerEventArgs e)
        {
            DebugLogger(e.controllerIndex, "POINTER IN", e.target, e.raycastHit, e.distance, e.destinationPosition);
			if (e.target.name.Equals ("Free_Play") && !GameObject.Find("Hover").GetComponent<Image>().enabled && !sel.selected.Equals("Free Play")) {
				GameObject.Find ("Hover").transform.localPosition = new Vector3 (3.8f, 2.62f, 0);
				GameObject.Find ("Hover").GetComponent<Image> ().enabled = true;
			} else if (e.target.name.Equals ("Time_Attack") && !GameObject.Find("Hover").GetComponent<Image>().enabled && !sel.selected.Equals("Time Attack")) {
				GameObject.Find ("Hover").transform.localPosition = new Vector3 (-5.54f, 2.62f, 0);
				GameObject.Find ("Hover").GetComponent<Image> ().enabled = true;
			}
        }

        private void DoPointerOut(object sender, DestinationMarkerEventArgs e)
        {
            DebugLogger(e.controllerIndex, "POINTER OUT", e.target, e.raycastHit, e.distance, e.destinationPosition);
			if (e.target.name.Equals ("Free_Play") && GameObject.Find("Hover").GetComponent<Image>().enabled) {
				GameObject.Find ("Hover").GetComponent<Image> ().enabled = false;
			} else if (e.target.name.Equals ("Time_Attack") && GameObject.Find("Hover").GetComponent<Image>().enabled) {
				GameObject.Find ("Hover").GetComponent<Image> ().enabled = false;
			}
        }

        private void DoPointerDestinationSet(object sender, DestinationMarkerEventArgs e)
        {
            DebugLogger(e.controllerIndex, "POINTER DESTINATION", e.target, e.raycastHit, e.distance, e.destinationPosition);
			if (e.target.name.Equals ("Free_Play")) {
				GameObject.Find ("Selected").transform.localPosition = new Vector3 (3.8f, 2.62f, 0);
				GameObject.Find ("Hover").GetComponent<Image> ().enabled = false;
				sel.selected = "Free Play";
			} else if (e.target.name.Equals ("Time_Attack")) {
				GameObject.Find ("Selected").transform.localPosition = new Vector3 (-5.54f, 2.62f, 0);
				GameObject.Find ("Hover").GetComponent<Image> ().enabled = false;
				sel.selected = "Time Attack";
			}
        }
    }
}