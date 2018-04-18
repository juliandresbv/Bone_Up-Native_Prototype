using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour {

	private bool camAvailable;
	private WebCamTexture backCamera;
	private Texture defaultBackground;

	public RawImage background;
	public AspectRatioFitter fit;

	// Use this for initialization
	void Start () {
		defaultBackground = background.texture;
		WebCamDevice[] devices = WebCamTexture.devices;

		checkCameras(devices);
		initBackCamera(devices);

		/*
		if (devices.Length == 0) {
			Debug.Log ("No camera detected");
			camAvailable = false;
			return;
		}

		for (int i = 0; i < devices.Length; i++) {
			if (!devices [i].isFrontFacing) {
				backCamera = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
			}
		}

		if (backCamera == null) {
			Debug.Log ("Unable to find back camera");
			return;
		}

		backCamera.Play();
		background.texture = backCamera;
		camAvailable = true;
		*/
	}

	//Evalua la disponibilidad/existencia de camaras en el dispositivo
	private void checkCameras(WebCamDevice[] deviceCameras) {
		if (deviceCameras.Length == 0) {
			Debug.Log ("No camera detected");
			camAvailable = false;
			return;
		}
	}

	//Inicializa la camara trasera
	private void initBackCamera(WebCamDevice[] deviceCameras) {
		for (int i = 0; i < deviceCameras.Length; i++) {
			if (!deviceCameras[i].isFrontFacing) {
				backCamera = new WebCamTexture(deviceCameras[i].name, Screen.width, Screen.height);
			}
		}

		if (backCamera == null) {
			Debug.Log ("Unable to find back camera");
			return;
		}

		backCamera.Play();
		background.texture = backCamera;
		camAvailable = true;
	}

	// Update is called once per frame
	void Update () {
		if(!camAvailable) {
			return;
		}

		float ratio = ((float) backCamera.width) / ((float) backCamera.height);
		fit.aspectRatio = ratio;

		float scaleY = backCamera.videoVerticallyMirrored ? -1f : 1f;
		background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

		int orient = -backCamera.videoRotationAngle;
		background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
	}
}
