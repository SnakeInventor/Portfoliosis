import { useMemo, useState } from "react"
import MySkillCard from "./MySkillCard"
import enTextData from "../assets/localisation/Skills/en-us.json"
import ruTextData from "../assets/localisation/Skills/ru-ru.json"

type MySkillsProps = {
  language: "ru-ru" | "en-us"
}

export default function MySkills( {language} : MySkillsProps) {
  const [activeTab, setActiveTab] = useState("Backend");
  
  const textData = useMemo(() => (language === "ru-ru") ?  ruTextData : enTextData, [language])

  return (
    <section className='skills' id="skills">
      <div className="skills__text-container">
        <div className="skills__text">
          <h2 className="skills__text-title">{textData.title}</h2>
          <h4 className="skills__text-description">{textData.description}</h4>
        </div>
      </div>
      <div className="skills__cards">
        <ul className="skills__cards-tabs">
        {
          textData.tabs.map((tab) => {
            return <li 
              key={tab.title} 
              className={"skills__cards-tabs-tab" + (activeTab === tab.title ? " skills__cards-tabs-tab--active" : "")}
              onClick={() => setActiveTab(tab.title)}
            >
              {tab.title}
            </li>}
          )
        }
        </ul>
        <div className="skills__cards-container">
          <ul className="skills__cards-list">
          {
              textData.tabs.find(tab => tab.title === activeTab)?.cards.map((card) => (
                <li className="skills__cards-list-item" key={card.title} >
                  <MySkillCard title={card.title} icon={card.icon} description={card.description}/>
                </li>
              ))
          }
          </ul>
        </div>  
      </div>
    </section>
  )
}


