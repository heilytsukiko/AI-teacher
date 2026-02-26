// import 'dom-speech-recognition';

//нужно, чтобы TS не ругался на отсутсвие webkitSpeechRecognition в стандартных типах window
interface Window {
    SpeechRecognition: typeof SpeechRecognition;
    webkitSpeechRecognition: typeof SpeechRecognition;

    SpeechRecognitionEvent: typeof SpeechRecognitionEvent;
    webkitSpeechRecognitionEvent: typeof SpeechRecognitionEvent;
}