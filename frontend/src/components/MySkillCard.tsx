type MySkillCardProps = {
  title: string;
  icon: string;
  description: string;
}

export default function MySkillCard({title, icon, description}: MySkillCardProps) {
  return (
    <div className="skillcard">
    <div className="skillcard__label">
      <div className="skillcard__label-icon">
        <img className="skillcard__label-icon-image" src={icon}/>
      </div>               
      <h3 className="skillcard__label-title">{title}</h3>
    </div>
    <p className="skillcard__text">{description}</p>
  </div>
  )
}