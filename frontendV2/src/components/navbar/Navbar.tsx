import { FC } from 'react';
import { Link, useLocation } from 'react-router-dom';
import { GoHeart, GoPerson } from 'react-icons/go';
import { cn } from 'lib/utils';
import Sheet, { SheetTrigger } from 'components/sheet';
import Button from 'components/button';
import MenuIcon from 'components/navbar/icons/MenuIcon';
import { SheetContent } from 'components/sheet/Sheet';

const NavbarPages: { title: string; to: string }[] = [
  { title: 'Home', to: '/' },
  { title: 'Uploads', to: '/uploads' },
  { title: 'Matches', to: '/matches' },
  { title: 'Favourites', to: '/favourites' },
];

const Navbar: FC = () => {
  const currentPath = useLocation().pathname;

  return (
    <header className="h-12 sm:h-24 flex flex-col bg-background">
      <div className="flex-1 flex w-full justify-center font-butler">
        <div className="flex-1 flex justify-between items-center max-w-content">
          <Link to="#" className="font-medium text-2xl">
            Koral
          </Link>
          <nav className="flex gap-5 items-center">
            <GoHeart fontSize="18" />
            <GoPerson fontSize="18" />
            <Sheet modal={false}>
              <SheetTrigger asChild>
                <Button
                  variant="outline"
                  size="icon"
                  className="sm:hidden border-0 shadow-none bg-transparent h-5 w-5"
                >
                  <MenuIcon fontSize="18" />
                </Button>
              </SheetTrigger>
              <SheetContent side="right">
                {NavbarPages.map((page) => {
                  return (
                    <Link
                      to={page.to}
                      className="flex w-full items-center py-4 text-lg"
                      key={page.to}
                    >
                      {page.title}
                    </Link>
                  );
                })}
              </SheetContent>
            </Sheet>
          </nav>
        </div>
      </div>

      <div className="hidden sm:flex flex-1 border  items-center justify-center">
        <div className="flex-1 flex gap-x-8 text-[13px] max-w-content justify-start h-full items-center">
          {NavbarPages.map((page) => {
            const isActive = currentPath === page.to;

            return (
              <Link
                key={page.to}
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
