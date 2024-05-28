# Flappy bird

Игра на Unity/C#

Классическая игра Flappy Bird. На вашего персонажа постоянно движутся трубы, вам нужно прыгать между ними, нажимая на пробел. Ваша задача - как можно дольше не врезаться ни в одну из них. Врезался - взорвался и проиграл. За каждый раз, когда вы успешно проскочите между трубами, вам будет начисляться одно очко. Можно играть одному, чтобы "убить время", либо соревноваться с друзьями, кто сможет набрать больше очков.

![Начало игры](https://github.com/kosobutski/uch_practice/blob/main/screenshots/up1.png)
![Процесс игры](https://github.com/kosobutski/uch_practice/blob/main/screenshots/up2.png)
![Окно проигрыша](https://github.com/kosobutski/uch_practice/blob/main/screenshots/up3.png)
![Меню с правилами игры](https://github.com/kosobutski/uch_practice/blob/main/screenshots/up4.png)

Проект был создан на базе движка Unity с применением языка программирования C#. Он состоит из окна меню (по факту это окно, где описываются правила игры в шуточной форме) и окна игры, где происходит сам геймплей. В окне меню находятся объекты текста и кнопка начала игры. В окне игры всё сложнее - объекты есть видимые, невидимые и объекты-менеджеры. Видимые - игрок, пол, потолок, постоянно спавнящиеся трубы и текст, отображающий счёт. Невидимые - окно проигрыша (при проигрыше оно становится видимым) и спавнер труб. К объектам-менеджерам можно отнести EventSystem и GameManager - они регулируют процессы, происходящие внутри игры.

Теперь пройдёмся по скриптам. Их всего 5.

# Player.cs

Здесь прописана механика работы игрока. При нажатии на пробел или на левую кнопку мыши он подпрыгивает.

```
if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
{
    rb.AddForce((transform.up * jumpForce), ForceMode2D.Impulse);
}
```

Также при столкновении с трубой (объектом под тегом "PipePart") игра оканчивается - запускается функция Lose, которая будет описана позже.

```
if (collision.gameObject.CompareTag("PipePart"))
{
    GameManager.instance.Lose();
}
```

# Pipe.cs

Объектом Pipe считаются две трубы, стоящие вертикально друг напротив друга, которые постоянно едут влево (в сторону персонажа), а персонаж по факту просто подпрыгивает на месте. Это создаёт иллюзию того, что персонаж летит в сторону труб.

```
transform.Translate(Vector2.left * speed * Time.deltaTime);
```

При пролёте между трубами счёт увеличивается на единицу.

```
private void OnTriggerEnter2D(Collider2D collision)
{
    if(collision.TryGetComponent<Player>(out Player player))
    {
        ScoreManager.Instance.SetScore(1);
    }
}
```

# Spawner.cs

Это скрипт, который находится справа от поля игры и создаёт объекты Pipe, экземпляр такого объекта находится по пути UPractice\Assets\Prefab. Он генерирует трубы, где позиция X постоянно меняется, а позиция Y генерируется случайным образом, при этом есть минимальное и максимальное значение Y, которое труба может принимать. По умолчанию Y принимает позицию от -3 до 3. Время спавна труб также установлено по умолчанию.

```
private void Update()
{
    if (timer <= 0)
    {
        timer = timeToSpawn;
        GameObject pipe = Instantiate(pipePrefab, transform.position, Quaternion.identity);
        float rand = Random.Range(minYPosition, maxYPosition);
        pipe.transform.position = new Vector3(pipe.transform.position.x, rand, 0);
    }
    else
    {
        timer -= Time.deltaTime;
    }
}
```

# ScoreManager.cs

Здесь устанавливается текущий счёт игрока, который увеличивается на единицу каждый раз, когда игрок проскакивает между труб, и выводится на экран в текстовом виде. В этом же скрипте воспроизводится звук coinAudio - звук увеличения счёта. Ниже представлена отвечающая за это всё функция setScore, принимающая на вход значение счёта.

```
public void SetScore(int score)
{
    this.score += score;
    scoreText.text = "Счёт: " + this.score.ToString();
    coinAudio.Play();
}
```

# GameManager.cs

