# Carefull with white space use tabs and don't have Trailing Spaces after Backslash
# Create a New Rust library in the directory: /src_backend/api_interface_rust
#
# uses /bin/sh 	currently used
#   # read -n 1 -s -r -p "Press any key to continue..."  only for /bin/bash
# to use bash : SHELL := /bin/bash			 # Can't use breaks with nelines and tabs
#
create-lib:
	@echo ""; \
	echo " CREATE a New Rust library.  "; \
	echo " ---------------------------"; \
	echo ""; \
	echo "Enter the Rust library name. Note you ***must*** use the name convention: 'your_name_lib' (see REMARK 1 in cargo.toml of 'core_lib')  "; \
	read -p " *** INPUT REQUIRED ***    Enter a new Rust library name for: ./src_backend/api_interface_rust/ : " libname; \
	echo ""; \
	echo ""; \
	cd $(DIR)/src_backend/api_interface_rust && \
	cargo new $$libname --lib  || exit 1;  \
	printf '\n[lib]\ncrate-type = ["cdylib"]\n' >> $$libname/Cargo.toml; \
	echo ""; \
	echo ""; \
	echo "SUCCESSFUL!!"; \
	echo "    - If you need to remove the library remove the directory and remove the reference in" ; \
	echo "      *** workspace *** space 'cargo.toml' in ./src_backend"; \
	echo ""; \
	echo ""; \
	echo "Press Enter to continue..."; \
	read dummy


create-test_lib:
	@echo ""; \
	echo " CREATE a New Test Client to test a Rust library.  "; \
	echo " --------------------------------------------------"; \
	echo ""; \
	echo ""; \
	echo "\t -MAKE SURE that the libray name in the cargo.toml is the same as the directory name!!	"; \
	read -p "     *** INPUT REQUIRED ***    Enter an existing 'library directory name' of: ./src_backend/api_interface_rust/ :" libname; \
	echo ""; \
	echo ""; \
    if [ ! -d "$(DIR)/src_backend/api_interface_rust/$$libname" ]; then \
        echo "\t***ERROR***\n\n\t - That library directory does not exists. Enter a library that exists in: './src_backend/api_interface_rust/'\n\t   I can't be bothered, aborting!\n\n\n"; \
        exit 1; \
    fi ;\
	cd $(DIR)/src_backend/api_interface_rust && \
	cargo new --bin tests/test_$$libname || exit 1; \
	echo "$$libname = {path = \"../../$$libname\"} " >> $(DIR)/src_backend/api_interface_rust/tests/test_$$libname/Cargo.toml; \
	echo ""; \
	echo "use $$libname::*; \n\nfn main()\n{\n\tlet result = add(110, 11);\n\tprintln!(\"Function succeeded with result: {}\",result);\n} " > $(DIR)/src_backend/api_interface_rust/tests/test_$$libname/src/main.rs; \
	echo ""; \
	echo ""; \
	echo " SUCCESSFULLY CREATED! Your library test program:'test_$$libname' \n"; \
	echo "\t 1. Test file\t\t\t\t: $(DIR)/src_backend/api_interface_rust/tests/test_$$libname/src/main.rs"; \
	echo "\t  \t- Using lib\t\t\t: $(DIR)/src_backend/api_interface_rust/$$libname/"; \
	echo "\t 2. Updated workspace cargo\t\t: $(DIR)/src_backend/Cargo.toml"; \
	echo "\t 3. Use VSC \t\t\t\t: To Build: 'Terminal -> Tasks -> 'AFX BUILD LIB. TEST' "; \
	echo "\t    You need to adjust the path(cwd) to the directory containing the 'Carggo.toml' file in the './vscode/tasks.json' file "; \
	echo "\t 4. Use VSC \t\t\t\t: 'RUN AND DEBUG > AFX RUN Library Test. to run it "; \
	echo "\t    You need to adjust the path to the main.rs for the section in the './vscode/launch.json' file "; \
	echo ""; \
	echo "\t - Remark: If you need to remove the library remove the directory and remove the reference under:" ; \
	echo "\t   ***workspace*** space 'cargo.toml' in ./src_backend"; \
	echo ""; \
	echo "\t - Note: The workspace Cargo.toml is checked during this program (/src_backend/Cargo.toml), so an error" ; \
	echo "\t -       that may occur does not necessarily have to come from this script; it may have been introduced earlier " ; \
	echo ""; \
	echo "Press Enter to continue..."; \
	read dummy
