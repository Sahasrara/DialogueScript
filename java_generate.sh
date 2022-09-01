#/bin/bash

# Variables
OUTPUT_DIR="java_parser"
CLASSPATH=".:./antlr/antlr-4.10.1-complete.jar"

# Create Output Directory
[ -d $OUTPUT_DIR ] && rm -rf $OUTPUT_DIR
mkdir $OUTPUT_DIR 

# Create Test Rig
TEST_FILE="./test.sh"
[ -d $TEST_FILE ] && rm $TEST_FILE
cat > $TEST_FILE <<- EOM
#/bin/bash
OUTPUT_DIR="generated_java"
CLASSPATH=".:./antlr/antlr-4.10.1-complete.jar:./$OUTPUT_DIR"
java -cp \$CLASSPATH org.antlr.v4.gui.TestRig DialogueScript script -gui example_script.ds
EOM
chmod +x $TEST_FILE

# Generate Parser
java -cp $CLASSPATH org.antlr.v4.Tool DialogueScriptLexer.g4 DialogueScriptParser.g4 \
    -o ./$OUTPUT_DIR/

# Compile
javac -cp $CLASSPATH  \
    -s ./$OUTPUT_DIR/ \
    ./$OUTPUT_DIR/*.java