using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]//Assegurar que o AudioSource estara presente
public class MorseAudioPlayer : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private float frequency = 700f; // tom rádio
    [SerializeField] private float volume = 0.2f;

    [Header("Timing")]
    [SerializeField] private float unitTime = 0.08f; // velocidade do morse

    private AudioSource source;

    Dictionary<char, string> morseTable = new Dictionary<char, string>()
    {
        {'A', ".-"}, {'B', "-..."}, {'C', "-.-."}, {'D', "-.."},
        {'E', "."}, {'F', "..-."}, {'G', "--."}, {'H', "...."},
        {'I', ".."}, {'J', ".---"}, {'K', "-.-"}, {'L', ".-.."},
        {'M', "--"}, {'N', "-."}, {'O', "---"}, {'P', ".--."},
        {'Q', "--.-"}, {'R', ".-."}, {'S', "..."}, {'T', "-"},
        {'U', "..-"}, {'V', "...-"}, {'W', ".--"}, {'X', "-..-"},
        {'Y', "-.--"}, {'Z', "--.."},
        {'1', ".----"}, {'2', "..---"}, {'3', "...--"},
        {'4', "....-"}, {'5', "....."}, {'6', "-...."},
        {'7', "--..."}, {'8', "---.."}, {'9', "----."}, {'0', "-----"}
    };

    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
    }

    /*public void Play(string message)// MÉTODO PRINCIPAL
    {
        StopAllCoroutines();
        StartCoroutine(PlayMorse(message.ToUpper()));
    }*/

    public void PlayRealtime(string message, TMPro.TextMeshProUGUI output)
    {
        StopAllCoroutines();
        StartCoroutine(PlayAndWrite(message.ToUpper(), output));
    }
    
    IEnumerator PlayAndWrite(string msg, TMPro.TextMeshProUGUI output)
    {
        output.text = "";

        foreach (char c in msg)
        {
            if (c == ' ')
            {
                output.text += "\n";
                yield return new WaitForSeconds(unitTime * 7);
                continue;
            }

        if (!morseTable.ContainsKey(c))
            continue;

        string code = morseTable[c];

        foreach (char symbol in code)
            {
                float duration = (symbol == '.') ? unitTime : unitTime * 3;

                output.text += symbol;

                yield return StartCoroutine(Beep(duration));

                yield return new WaitForSeconds(unitTime);
            }

            output.text += " ";
            yield return new WaitForSeconds(unitTime * 2);
        }
    }

    /*IEnumerator PlayMorse(string msg)//Toca a mensagem
    {
        foreach (char c in msg)
        {
            if (c == ' ')
            {
                yield return new WaitForSeconds(unitTime * 7);
                continue;
            }

            if (!morseTable.ContainsKey(c))
                continue;

            string code = morseTable[c];

            foreach (char symbol in code)
            {
                float duration = (symbol == '.') ? unitTime : unitTime * 3;

                yield return StartCoroutine(Beep(duration));
                yield return new WaitForSeconds(unitTime);
            }

            yield return new WaitForSeconds(unitTime * 2);
        }
    }*/

    IEnumerator Beep(float duration)//O som
    {
        source.clip = GenerateTone(duration);
        source.Play();
        yield return new WaitForSeconds(duration);
    }

    AudioClip GenerateTone(float duration)//Gera beep procedural
    {
        int sampleRate = 44100;
        int samples = (int)(sampleRate * duration);

        float[] data = new float[samples];

        for (int i = 0; i < samples; i++)
        {
            data[i] = Mathf.Sin(2 * Mathf.PI * frequency * i / sampleRate) * volume;
        }

        AudioClip clip = AudioClip.Create("tone", samples, 1, sampleRate, false);
        clip.SetData(data, 0);

        return clip;
    }
    
    public string GenerateRandomCoordinates()
    {
        int x = Random.Range(0, 66);//  Latitude
        int y = Random.Range(35, 76);// Longitude

        return $"{x} {y}";
    }

    public string BuildFullMorseMessage(string baseMessage)
    {
        string coords = GenerateRandomCoordinates();

        string morseText = ConvertToMorseString(baseMessage);
        string morseCoords = ConvertToMorseString(coords);

        return morseText + "\n\n" + morseCoords;
    }

    public string ConvertToMorseString(string message)//Convete a mensagem para codigo morse
    { 
        message = message.ToUpper();

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        foreach (char c in message)
        { 
            if (c == ' ')
            {
                sb.Append("/ ");
                continue;
            }

            if (!morseTable.ContainsKey(c))
                continue;

                sb.Append(morseTable[c]);
                sb.Append(" ");
            }

        return sb.ToString();
    }

}
