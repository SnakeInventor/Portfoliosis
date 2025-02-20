import MyShield from './MyShield'
import { MyShieldProps } from './MyShield'


type MyProjectCardLinkProps = {
  href: string,
  image: string
}

type MyProjectCardProps = {
  title: string,
  image: string,
  links: MyProjectCardLinkProps[],
  shields: MyShieldProps[],
  description: string,
  timestamp: string
}



export default function MyProjectCard({title, image, links, shields, description, timestamp}: MyProjectCardProps) {

  return (
    <div className='projectcard'>
      <div className="projectcard__picture">
        <img className="projectcard__picture-image" src={image}></img>
      </div>
      <div className="projectcard__text">
        <div className="projectcard__text-title">
          <div className="projectcard__text-title-text">{title}</div>
          <ul className="projectcard__text-links-list">
            {
              links.map((link) => 
              <li key={link.href + link.image} className="projectcard__text-links-list-item">
                <a className="projectcard__text-links-link" href={link.href}>
                  <img className="projectcard__text-links-link-image" src={link.image}></img>
                </a>
              </li>
              )
            }
          </ul>
        </div>
        <div className="projectcard__text-shields">
          <ul className="projectcard__text-shields-list">
            {
              shields.map(shield => 
              <li key={shield.text} className="projectcard__text-shields-list-item">
                <MyShield text={shield.text} image={shield.image} color={shield.color}/>
              </li>
              )
            }
          </ul>
        </div>
        <div className="projectcard__text-description">{description}</div>
      </div>
      <div className="projectcard__timestamp">{timestamp}</div>
    </div>
  )
}