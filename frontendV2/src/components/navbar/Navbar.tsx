import { FC, useCallback, useLayoutEffect, useRef, useState } from 'react';
import { Link, useLocation } from 'react-router-dom';
import { GoBell, GoPerson } from 'react-icons/go';
import { cn } from 'lib/utils';
import Sheet, { SheetTrigger } from 'components/sheet';
import Button from 'components/button';
import MenuIcon from 'components/navbar/icons/MenuIcon';
import { SheetContent } from 'components/sheet/Sheet';
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuGroup,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuShortcut,
  DropdownMenuTrigger,
} from 'components/dropdownMenu';
import globalRouter from 'App/globalRouter';

const UserNav: FC = () => {
  const handleLogout = () => {
    globalRouter.navigate?.('/login');
  };

  return (
    <DropdownMenu>
      <DropdownMenuTrigger asChild>
        <Button
          variant="ghost"
          className="border-0 p-2 rounded-full focus-visible:ring-0"
        >
          <GoPerson fontSize="18" />
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent className="w-56" align="end" forceMount>
        <DropdownMenuLabel className="font-normal">
          <div className="flex flex-col space-y-1">
            <p className="text-sm font-medium leading-none">marco</p>
            <p className="text-xs leading-none text-muted-foreground">
              hool@example.com
            </p>
          </div>
        </DropdownMenuLabel>
        <DropdownMenuSeparator />
        <DropdownMenuGroup>
          <DropdownMenuItem>
            Profile
            <DropdownMenuShortcut>⇧⌘P</DropdownMenuShortcut>
          </DropdownMenuItem>
          <DropdownMenuItem>
            Billing
            <DropdownMenuShortcut>⌘B</DropdownMenuShortcut>
          </DropdownMenuItem>
          <DropdownMenuItem>
            Settings
            <DropdownMenuShortcut>⌘S</DropdownMenuShortcut>
          </DropdownMenuItem>
          <DropdownMenuItem>New Team</DropdownMenuItem>
        </DropdownMenuGroup>
        <DropdownMenuSeparator />
        <DropdownMenuItem onClick={handleLogout}>
          Log out
          <DropdownMenuShortcut>Ctrl+L</DropdownMenuShortcut>
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>
  );
};

const NavbarPages: { title: string; to: string }[] = [
  { title: 'Home', to: '/' },
  { title: 'Uploads', to: '/uploads' },
  { title: 'Matches', to: '/matches' },
  { title: 'Favourites', to: '/favourites' },
];

const Navbar: FC = () => {
  const currentPath = useLocation().pathname;
  const linkRefs = useRef<(HTMLAnchorElement | null)[]>([]);
  const [activeLinkPosition, setActiveLinkPosition] = useState<{
    left: number;
    width: number;
  }>({ left: 0, width: 0 });
  const [activeLinkOpacity, setActiveLinkOpacity] = useState(0);

  const updateActiveLinkPosition = useCallback(() => {
    const activeLinkIndex = NavbarPages.findIndex(
      (page) => currentPath === page.to,
    );

    if (activeLinkIndex !== -1) {
      const activeLink = linkRefs.current[activeLinkIndex];
      if (activeLink) {
        setActiveLinkPosition({
          left: activeLink.offsetLeft,
          width: activeLink.offsetWidth,
        });
        setActiveLinkOpacity(1);
      }
    } else {
      setActiveLinkOpacity(0);
    }
  }, [currentPath]);

  useLayoutEffect(() => {
    updateActiveLinkPosition();

    const handleResize = () => updateActiveLinkPosition();
    window.addEventListener('resize', handleResize);

    return () => window.removeEventListener('resize', handleResize);
  }, [currentPath, updateActiveLinkPosition]);

  return (
    <header className="h-12 sm:h-24 flex flex-col bg-background">
      <div className="flex-1 flex w-full justify-center font-butler">
        <div className="flex-1 flex justify-between items-center max-w-content">
          <Link to="#" className="font-medium text-2xl">
            Koral
          </Link>
          <nav className="flex gap-5 items-center">
            <GoBell fontSize="18" />
            <UserNav />
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

      <div className="relative hidden sm:flex flex-1 border  items-center justify-center">
        <div className="flex-1 flex gap-x-8 text-[13px] max-w-content justify-start h-full items-center">
          <span
            className="absolute top-0 h-[1px] bg-black transition-all duration-100"
            style={{
              left: `${activeLinkPosition.left}px`,
              width: `${activeLinkPosition.width}px`,
              opacity: activeLinkOpacity,
            }}
          />
          <span
            className="absolute bottom-0 h-[1px] bg-black transition-all duration-100"
            style={{
              left: `${activeLinkPosition.left}px`,
              width: `${activeLinkPosition.width}px`,
              opacity: activeLinkOpacity,
            }}
          />
          {NavbarPages.map((page, index) => {
            return (
              <Link
                key={page.to}
                to={page.to}
                className={cn('h-full items-center flex')}
                ref={(el) => (linkRefs.current[index] = el)}
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
