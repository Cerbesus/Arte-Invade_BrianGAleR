using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Voice;
using Meta.WitAi.Requests;

[RequireComponent(typeof(AppVoiceExperience))]
public class EscucharSiempre : MonoBehaviour
{
    private AppVoiceExperience voiceExperience;

    private void Start() {
        voiceExperience = GetComponent<AppVoiceExperience>();
        voiceExperience.VoiceEvents.OnComplete.AddListener(OnVoiceTranscription);
        voiceExperience.Activate();
    }

    public void OnVoiceTranscription(VoiceServiceRequest _) {
        voiceExperience.Activate();
    }
}
