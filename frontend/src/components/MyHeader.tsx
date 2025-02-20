import { useMemo } from "react"
import language_icon from "../assets/change_language__icon.png"
import enTextData from "../assets/localisation/Header/en-us.json"
import ruTextData from "../assets/localisation/Header/ru-ru.json"

type MyHeaderProps = {
  language: "ru-ru" | "en-us",
  onLanguageButtonClick: React.MouseEventHandler<HTMLButtonElement>
}

export default function MyHeader({language, onLanguageButtonClick} :MyHeaderProps) {

  const textData = useMemo(() => (language === "ru-ru") ?  ruTextData : enTextData, [language])

  return (
    <header className="header" id="header">
    <div className="header__container">
      <div className="header__logo-wrapper">
        SNAKEINVENTOR
      </div>
      <div className="header__logo-wrapper-mobile">
        SI
      </div>
      <nav className="header__navigation">
        <ul className="header__navigation-list">
          {
            textData.navigation.map((link) => 
              <li key={link.text}><a href={link.link}>{link.text}</a></li>
            )
          }
        </ul>
      </nav>
      <button className="header__reset-language" onClick={onLanguageButtonClick}>
        <div className="header__reset-language-container">
          <img className="header__reset-language-image" src={language_icon}/>
        </div>
      </button>
    </div>
  </header>
  )
}