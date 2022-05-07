using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // её подключаем

public class Scenes : MonoBehaviour
{
    public void ChangeScenes(int numberScene) // метод для смены сцен с параметром
    {
        SceneManager.LoadScene(numberScene); // сама смена сцены через параметр
    }

    public void Exit() // метод для выхода из игры
    {
        Application.Quit(); // функция выхода
    }
}
