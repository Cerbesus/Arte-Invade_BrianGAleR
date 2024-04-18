using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
// Instancia única del AudioManager (porque es una clase Singleton) STATIC
  public static AudioManager instance;

  // Se crean dos AudioSources diferentes para que se puedan
  // reproducir los efectos y la música de fondo al mismo tiempo
  public AudioSource sfxSource; // Componente AudioSource para efectos de sonido
  public AudioSource musicSource; // Componente AudioSource para la música de fondo

  // En vez de usar un vector de AudioClips (que podría ser) vamos a utilizar un Diccionario
  // en el que cargaremos directamente los recursos desde la jerarquía del proyecto
  // Cada entrada del diccionario tiene una string como clave y un AudioClip como valor
  public Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();
  public Dictionary<string, AudioClip> musicClips = new Dictionary<string, AudioClip>();

  // Método Awake que se llama al inicio antes de que se active el objeto. Útil para inicializar
  // variables u objetos que serán llamados por otros scripts (game managers, clases singleton, etc).
  private void Awake() {

    // ----------------------------------------------------------------
    // AQUÍ ES DONDE SE DEFINE EL COMPORTAMIENTO DE LA CLASE SINGLETON
    // Garantizamos que solo exista una instancia del AudioManager
    // Si no hay instancias previas se asigna la actual
    // Si hay instancias se destruye la nueva
    if (instance == null) instance = this;
    else { Destroy(gameObject); return; }
    // ----------------------------------------------------------------

    // No destruimos el AudioManager aunque se cambie de escena
    DontDestroyOnLoad(gameObject);

    // Cargamos los AudioClips en los diccionarios
    LoadSFXClips();
    LoadMusicClips();

  }

  private void Start() {
    // Configuramos el volumen de la música de fondo
    musicSource.volume = PlayerPrefs.GetFloat("BackgroundMusicVolume", 0.5f);
    // Configuramos el volumen de los efectos de sonido
    sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
    // Configuramos que la se ejecute la musica que este guardada en player prefs
    PlayMusic(PlayerPrefs.GetString("BackgroundMusicName", "Cosmic Odyssey"));
  }

  // Método privado para cargar los efectos de sonido directamente desde las carpetas
  private void LoadSFXClips() {
    // Los recursos (ASSETS) que se cargan en TIEMPO DE EJECUCIÓN DEBEN ESTAR DENTRO de una carpeta denominada /Assets/Resources/SFX
      sfxClips["GetGem"] = Resources.Load<AudioClip>("SFX/getGem");
  }

  // Método privado para cargar la música de fondo directamente desde las carpetas
  private void LoadMusicClips() {
      // Los recursos (ASSETS) que se cargan en TIEMPO DE EJECUCIÓN DEBEN ESTAR DENTRO de una carpeta denominada /Assets/Resources/Music
      musicClips["Galactic Drift"] = Resources.Load<AudioClip>("Music/BackgroundMusic_1");
      musicClips["Cosmic Odyssey"] = Resources.Load<AudioClip>("Music/BackgroundMusic_2");
      musicClips["Galaxy Echoes"] = Resources.Load<AudioClip>("Music/BackgroundMusic_3");
      musicClips["Rick Roll"] = Resources.Load<AudioClip>("Music/BackgroundMusic_4");
      musicClips["Easter Eggcellent"] = Resources.Load<AudioClip>("Music/BackgroundMusic_5");
  }
  
  // Método de la clase singleton para reproducir efectos de sonido
  public void PlaySFX(string clipName) {
    if (sfxClips.ContainsKey(clipName)) {
      sfxSource.clip = sfxClips[clipName];
      sfxSource.Play();
    } else Debug.LogWarning("El AudioClip " + clipName + " no se encontró en el diccionario de sfxClips.");
	}

  // Método de la clase singleton para reproducir música de fondo
  public void PlayMusic(string clipName) {
    if (musicClips.ContainsKey(clipName) && musicSource.clip != musicClips[clipName]) {
      musicSource.clip = musicClips[clipName];
      musicSource.Play();
    } else Debug.LogWarning("El AudioClip " + clipName + " no se encontró en el diccionario de musicClips o ya se está reproduciendo.");
  }
}

// AudioManager.instance.PlayMusic("MainTheme");
// AudioManager.instance.PlaySFX("Jump");
