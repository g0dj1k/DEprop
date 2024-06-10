import telebot
from pymongo import MongoClient

bot = telebot.TeleBot("7394372121:AAFe_3ZX2ZXRYnseouMkmTD5ewnSKlGrDKI")


@bot.message_handler(commands=["start"])
def start(message):
    markup = telebot.types.ReplyKeyboardMarkup()
    btn1 = telebot.types.KeyboardButton('Обучающий материал')
    btn2 = telebot.types.KeyboardButton('Репозиторий')
    markup.row(btn1, btn2)
    bot.send_photo(message.chat.id, "https://sun9-6.userapi.com/impg/jL9zTw-z5hR0KuQXRdp6ZOKQRtMW8KsnCg5xcA/_xf5W2A3Py8.jpg?size=1024x1024&quality=96&sign=846563ab3f755d77b13dfc6586a4c413&type=album", '✌️Добро пожаловать в обучающую систему подготовки к демонстрационному экзамену! 📚💡                                                            '
                                          'Здесь вы найдете материалы для подготовки к экзамену. Не стесняйтесь изучать предложенный материал и использовать его для самостоятельного обучения. Наша цель - помочь вам достичь желаемого результата и уверенности в сдаче экзамена!👩‍🎓👨‍🎓                                                                                                '
                                          'Удачи в учебе!💪📝', reply_markup=markup)


@bot.message_handler()
def info(message):
    if message.text.lower() == 'обучающий материал':
        markup = telebot.types.InlineKeyboardMarkup()
        btn1 = telebot.types.InlineKeyboardButton('Перейти на сайт', url='https://nationalteam.worldskills.ru/skills/programmnye-resheniya-dlya-biznesa/')
        btn3 = telebot.types.InlineKeyboardButton('ОПИСАНИЕ КОМПЕТЕНЦИИ. ОБЗОР ОСНОВНОГО ПРОГРАММНОГО ОБЕСПЕЧЕНИЯ', url='https://nationalteam.worldskills.ru/skills/opisanie-kompetentsii-obzor-osnovnogo-programmnogo-obespecheniya/')
        btn4 = telebot.types.InlineKeyboardButton('ПРОЕКТИРОВАНИЕ USE CASE ДИАГРАММЫ. ОПРЕДЕЛЕНИЕ ФУНКЦИОНАЛЬНЫХ ВОЗМОЖНОСТЕЙ СИСТЕМЫ', url='https://nationalteam.worldskills.ru/skills/proektirovanie-use-case-diagrammy-opredelenie-funktsionalnykh-vozmozhnostey-sistemy/')
        btn5 = telebot.types.InlineKeyboardButton('СОЗДАНИЕ БАЗЫ ДАННЫХ', url='https://nationalteam.worldskills.ru/skills/sozdanie-bazy-dannykh/')
        btn6 = telebot.types.InlineKeyboardButton('СОЗДАНИЕ КАРКАСА ПРИЛОЖЕНИЯ. СОЗДАНИЕ И ИСПОЛЬЗОВАНИЕ СТИЛЕЙ', url='https://nationalteam.worldskills.ru/skills/sozdanie-karkasa-prilozheniya-sozdanie-i-ispolzovanie-stiley/')
        btn7 = telebot.types.InlineKeyboardButton('РАБОТА С БАЗОЙ ДАННЫХ В ПРИЛОЖЕНИИ: ЧТЕНИЕ, ДОБАВЛЕНИЕ, РЕДАКТИРОВАНИЕ, УДАЛЕНИЕ ДАННЫХ (ЧАСТЬ 1)', url='https://nationalteam.worldskills.ru/skills/rabota-s-bazoy-dannykh-v-prilozhenii-chtenie-dobavlenie-redaktirovanie-udalenie-dannykh-chast-1/')
        btn8 = telebot.types.InlineKeyboardButton('РАБОТА С БАЗОЙ ДАННЫХ В ПРИЛОЖЕНИИ: ЧТЕНИЕ, ДОБАВЛЕНИЕ, РЕДАКТИРОВАНИЕ, УДАЛЕНИЕ ДАННЫХ (ЧАСТЬ 2)', url='https://nationalteam.worldskills.ru/skills/rabota-s-bazoy-dannykh-v-prilozhenii-chtenie-dobavlenie-redaktirovanie-udalenie-dannykh-chast-2/')
        btn9 = telebot.types.InlineKeyboardButton('РАБОТА С НЕСТРУКТУРИРОВАННЫМИ ДАННЫМИ: ОБРАБОТКА И ИМПОРТ В БАЗУ ДАННЫХ', url='https://nationalteam.worldskills.ru/skills/rabota-s-nestrukturirovannymi-dannymi-obrabotka-i-import-v-bazu-dannykh/')
        btn11 = telebot.types.InlineKeyboardButton('МОДУЛЬНОЕ ТЕСТИРОВАНИЕ (UNIT-TESTS)', url='https://nationalteam.worldskills.ru/skills/modulnoe-testirovanie-unit-tests/')
        markup.row(btn1)
        markup.row(btn11, btn3)
        markup.row(btn4, btn5)
        markup.row(btn6, btn7)
        markup.row(btn8, btn9)
        bot.send_photo(message.chat.id, "https://sun9-6.userapi.com/impg/jL9zTw-z5hR0KuQXRdp6ZOKQRtMW8KsnCg5xcA/_xf5W2A3Py8.jpg?size=1024x1024&quality=96&sign=846563ab3f755d77b13dfc6586a4c413&type=album",  'Ниже предоставлен обучающий материал для демонстрационного экзамена. Переходя по ссылкам ниже, вы сможете ознакомиться с ним.📚                                                                  Этот материал может быть полезен вам при подготовке к экзамену. Удачи в учебе!🚀🏆', reply_markup=markup)
    if message.text.lower() == 'репозиторий':
        markup = telebot.types.InlineKeyboardMarkup()
        btn1 = telebot.types.InlineKeyboardButton('Репозиторий на github', url='https://github.com/g0dj1k/DEprop')
        markup.row(btn1)
        bot.send_photo(message.chat.id, "https://sun9-6.userapi.com/impg/jL9zTw-z5hR0KuQXRdp6ZOKQRtMW8KsnCg5xcA/_xf5W2A3Py8.jpg?size=1024x1024&quality=96&sign=846563ab3f755d77b13dfc6586a4c413&type=album", 'Для вашей подготовки к демонстрационному экзамену мы подготовили информационную систему и загрузили ее на GitHub. Перейдите по ссылке ниже, чтобы перейти к репозиторию с этим проектом.📚                                                       Эта информационная система включает в себя обучающий материал и тесты, которые помогут вам подготовиться к экзамену. Удачи в учебе!🚀🏆', reply_markup=markup)

bot.polling()