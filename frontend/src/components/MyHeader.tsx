
export default function MyHeader() {
  return (
    <header className="header">
    <div className="header__container">
      <div className="header__logo-wrapper">
        SNAKEINVENTOR
      </div>
      <nav className="header__navigation">
        <ul className="header__navigation-list">
          <li><a>Home</a></li>
          <li><a>Skills</a></li>
          <li><a>Projects</a></li>
          <li><a>Contact</a></li>
        </ul>
      </nav>
      <div className="header__dropdown-menu-wrapper">
      ≡
        <img>
        </img>
      </div>
    </div>
  </header>
  )
}