import { useMemo, useRef } from 'react'
import MyProjectCard from './MyProjectCard'
import enTextData from "../assets/localisation/Projects/en-us.json"
import ruTextData from "../assets/localisation/Projects/ru-ru.json"

type MyProjectProps = {
  language: "en-us" | "ru-ru"
}

export default function MyProjects({language}: MyProjectProps) {
  const textData = useMemo(() => (language === "ru-ru") ?  ruTextData : enTextData, [language]);

  const containerRef = useRef<HTMLDivElement>(null);
  const cardRefs = useRef<(HTMLLIElement | null)[]>([]);

  // this is horrible D:
  function moveToOutsideCard(toRight:boolean) {
    if (containerRef.current) {
      const cardVisibility: {card:HTMLElement, visible:boolean}[] = [];
      cardRefs.current.forEach((card) => {
        if (card && containerRef.current) {          
            cardVisibility.push({card:card, visible:!isElementOutsideHorizontaly(card, containerRef.current)});
        }
      });

      if (!toRight) {
        cardVisibility.reverse();
      }
      
      // find where visible cards start
      for (let firstIndex = 0; firstIndex < cardVisibility.length; firstIndex++) {
        const element = cardVisibility[firstIndex];
        console.log(firstIndex);
        if (element.visible) {
          // find where visible cards end
          for (let lastIndex = firstIndex + 1; lastIndex < cardVisibility.length; lastIndex++) {
            console.log(lastIndex);
            const element = cardVisibility[lastIndex];
            if (!element.visible) {
              // scroll the card into view
              element.card.scrollIntoView({block: "center", "behavior":"smooth"});
              break;
            }
          }
          break;
        }
      }
            
    }
  }
  

  return (
    <section className="projects" id='projects'>
      <div className="projects__text">
        <h2 className="projects__text-title">{textData.title}</h2>
        <h4 className="projects__text-description">{textData.description}</h4>
      </div>
      <div className="projects__cards" ref={containerRef}>
        <button 
          className="projects__cards-arrow projects__cards-arrow--left"
          onClick={() => {moveToOutsideCard(false)}}
        >{"<"}</button>
        <ul className="projects__cards-list">
          {
            textData.cards.map((card, index) => 
              <li key={card.title} className="projects__cards-list-item" ref={el => cardRefs.current[index] = el}>
                <MyProjectCard title={card.title} links={card.links} image={card.image}
                 shields={card.shields} description={card.description} timestamp={card.timestamp}/>
              </li>
            )
          }
        </ul>
        <button 
          className="projects__cards-arrow projects__cards-arrow--right"
          onClick={() => {moveToOutsideCard(true)}}
        >{">"}</button>
      </div>

    </section>
  )
}


function isElementOutsideHorizontaly (childElement: HTMLElement, containerElement: HTMLElement) {
  const childRect = childElement.getBoundingClientRect();
  const containerRect = containerElement.getBoundingClientRect();

  return (
    childRect.left < containerRect.left ||
    childRect.right > containerRect.right
  );
};