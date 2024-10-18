import { FC } from 'react';
import { Link, useLocation } from 'react-router-dom';
import { GoHeart, GoPerson } from 'react-icons/go';
import { cn } from 'lib/utils';

const NavbarPages: { title: string; to: string }[] = [
  { title: 'Home', to: '/' },
  { title: 'Uploads', to: '/uploads' },
  { title: 'Matches', to: '/matches' },
  { title: 'Favourites', to: '/favourites' },
];

const Navbar: FC = () => {
  const currentPath = useLocation().pathname;

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
        <div className="flex-1 flex gap-x-8 text-sm max-w-content justify-start h-full items-center">
          {NavbarPages.map((page) => {
            const isActive = currentPath === page.to;

            return (
              <Link
                to={page.to}
                className={cn(
                  'h-full items-center flex',
                  isActive && 'border-t border-b border-black',
                )}
              >
                <p className="p-2">{page.title}</p>
              </Link>
            );
          })}
        </div>
      </div>
    </header>
  );
};

export default Navbar;
