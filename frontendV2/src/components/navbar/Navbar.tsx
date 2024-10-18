import { FC } from 'react';
import { Link } from 'react-router-dom';
import { GoHeart, GoPerson } from 'react-icons/go';

const NavbarPages: { title: string; to: string }[] = [
  { title: 'Home', to: '/' },
  { title: 'Uploads', to: '/uploads' },
  { title: 'Matches', to: '/matches' },
  { title: 'Favourites', to: '/favourites' },
];

const Navbar: FC = () => {
  return (
    <header className="flex flex-col h-24 bg-background">
      <div className="flex-1 flex w-full justify-center font-butler">
        <div className="flex-1 flex justify-between items-center max-w-content">
          <Link to="#" className="font-medium text-2xl">
            Koral
          </Link>
          <nav className="flex gap-5">
            <GoHeart fontSize="18" />
            <GoPerson fontSize="18" />
          </nav>
        </div>
      </div>

      <div className="flex-1 border flex items-center justify-center">
        <div className="flex-1 flex gap-x-10 text-sm max-w-content justify-start">
          {NavbarPages.map((page) => (
            <Link to={page.to}>{page.title}</Link>
          ))}
        </div>
      </div>
    </header>
  );
};

export default Navbar;
