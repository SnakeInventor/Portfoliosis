import { useMemo } from "react"
import enTextData from "../assets/localisation/Header/en-us.json"
import ruTextData from "../assets/localisation/Header/ru-ru.json"

type MyHeaderProps = {
  language: "ru-ru" | "en-us"
}

export default function MyHeader({language} :MyHeaderProps) {

  const textData = useMemo(() => (language === "ru-ru") ?  ruTextData : enTextData, [language])

  return (
    <header className="header" id="header">
    <div className="header__container">
      <div className="header__logo-wrapper">
        SNAKEINVENTOR
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
      <div className="header__dropdown-menu-wrapper">
      ≡
      </div>
    </div>
  </header>
  )
}