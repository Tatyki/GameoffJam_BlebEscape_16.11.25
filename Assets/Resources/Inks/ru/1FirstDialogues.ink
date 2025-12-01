EXTERNAL Call(objectName, scriptName, methodName, param)

== Start ==
#name Spider
Эй, ты
Да, ты
Я чувствую запах настоящего гика
Наконец-то ты здесь
Мы уже подсуетились и собрали всё необходимое
Ты же уже в курсе происходящего?
-> My_Choices

== My_Choices ==
#color orange
#name none
 + [Rage quit (Ready to suffer)] -> Play
 + [You who? (Play training. You need it)] -> Lore

== Play ==
#name Spider
Если ты думал, что это кнопка для выхода, у меня для тебя плохие новости
Если же нет, я понял тебя, рандомный интернет исследователь
Иди же и спаси наш народ Блёбов от истребления
#anim BackGround_DisappearBackgroundAnim
Помни, наша нация рассчитывает на тебя
-> ToSecondLevelScene

== Lore ==
#name Spider
Так, слушай внимательно: повторять я не буду
Сейчас я расскажу тебе одну абсолютно правдивую историю о величайшей династии пауков, существовавшей здесь не одно десятилетие
Ты же слушаешь, да?
Помимо призыва гиковских духов мы умеем ещё делать и заговор на понос
Что? Почему у пауков из коробки больше власти, чем у тебя?
А вот мать свою слушать больше надо было
Ещё скажи, что яблоко, два года назад забытое тобой в контейнере, всё ещё там
Ладно, речь сейчас о делах намного более важных
Важнее нас с тобой
#anim BackGround_DisappearBackgroundAnim
А дело вот какое было:
Для первых завоевателей эта земля была райским оазисом
#anim ComicImage_ImageAppearAnim
~ Call ("ComicController", "ComicController", "SetComic", "1")
Из великих подпольных казюлей, съедобных остатков и кристаллов урановой глины из кошачьего наполнителя
Наши учёные создали своё лучшее детище
~ Call ("ComicController", "ComicController", "SetComic", "1")
Совершенное
Инновационное
Тупейшее
Или же просто идеальное
И имя ему было . . .
~ Call ("ComicController", "ComicController", "SetComic", "1")
БЛЁБ
~ Call ("ComicController", "ComicController", "SetComic", "1")
Ну и жили мы так спокойно долгое время
Вернее сказать, до прошлой недели, пока в доме не появилось оно
~ Call ("ComicController", "ComicController", "SetComic", "1")
Смердящее чудовище
~ Call ("ComicController", "ComicController", "SetComic", "1")
Правды ради, таким оно оставалось не долго
Но и после того лучше не стало
~ Call ("ComicController", "ComicController", "SetComic", "1")
Своей смертью оно породило адских созданий
Для простоты называем их Мушками
Но не дай им себя так просто обмануть
~ Call ("ComicController", "ComicController", "SetComic", "1")
Эти исчадия чьего-то больного сознания за время своего недолгого существования успели построить свой фашистский режим и устроить холокост нашим блёбам
Видите ли народность не может строиться на тупости
~ Call ("ComicController", "ComicController", "SetComic", "1")
Тем не менее, нам удалось сохранить малую часть выживших во временном убежище
Но нам следует поспешить
Слухи в нашем обществе распространяются очень быстро
Сейчас ты -- надежда всей нашей нации
Доведи выживших до бункера, где им ничто не сможет угрожать 
Если сможешь, получишь благодарность великой династии
#anim ComicImage_ImageDisappearAnim
Не подведи наш народ
~ Call ("ComicController", "ComicController", "SetComic", "1")
-> ToEducationScene


== ToEducationScene ==
~ Call ("SceneChanger", "DiaSceneChanger", "StartEducationComic", "1")
-> END

== ToSecondLevelScene ==
~ Call ("SceneChanger", "DiaSceneChanger", "StartSecondLevel", "1")
-> END