#
# Generated Makefile - do not edit!
#
# Edit the Makefile in the project folder instead (../Makefile). Each target
# has a -pre and a -post target defined where you can add customized code.
#
# This makefile implements configuration specific macros and targets.


# Include project Makefile
ifeq "${IGNORE_LOCAL}" "TRUE"
# do not include local makefile. User is passing all local related variables already
else
include Makefile
# Include makefile containing local settings
ifeq "$(wildcard nbproject/Makefile-local-default.mk)" "nbproject/Makefile-local-default.mk"
include nbproject/Makefile-local-default.mk
endif
endif

# Environment
MKDIR=gnumkdir -p
RM=rm -f 
MV=mv 
CP=cp 

# Macros
CND_CONF=default
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
IMAGE_TYPE=debug
OUTPUT_SUFFIX=elf
DEBUGGABLE_SUFFIX=elf
FINAL_IMAGE=dist/${CND_CONF}/${IMAGE_TYPE}/15.Ethernet.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}
else
IMAGE_TYPE=production
OUTPUT_SUFFIX=hex
DEBUGGABLE_SUFFIX=elf
FINAL_IMAGE=dist/${CND_CONF}/${IMAGE_TYPE}/15.Ethernet.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}
endif

# Object Directory
OBJECTDIR=build/${CND_CONF}/${IMAGE_TYPE}

# Distribution Directory
DISTDIR=dist/${CND_CONF}/${IMAGE_TYPE}

# Source Files Quoted if spaced
SOURCEFILES_QUOTED_IF_SPACED=ARP.s ICMP.s IP.s dsPicWEB.s enc.s ethernet.s http.s lcd.s memory.s tcp.s tcpip.s

# Object Files Quoted if spaced
OBJECTFILES_QUOTED_IF_SPACED=${OBJECTDIR}/ARP.o ${OBJECTDIR}/ICMP.o ${OBJECTDIR}/IP.o ${OBJECTDIR}/dsPicWEB.o ${OBJECTDIR}/enc.o ${OBJECTDIR}/ethernet.o ${OBJECTDIR}/http.o ${OBJECTDIR}/lcd.o ${OBJECTDIR}/memory.o ${OBJECTDIR}/tcp.o ${OBJECTDIR}/tcpip.o
POSSIBLE_DEPFILES=${OBJECTDIR}/ARP.o.d ${OBJECTDIR}/ICMP.o.d ${OBJECTDIR}/IP.o.d ${OBJECTDIR}/dsPicWEB.o.d ${OBJECTDIR}/enc.o.d ${OBJECTDIR}/ethernet.o.d ${OBJECTDIR}/http.o.d ${OBJECTDIR}/lcd.o.d ${OBJECTDIR}/memory.o.d ${OBJECTDIR}/tcp.o.d ${OBJECTDIR}/tcpip.o.d

# Object Files
OBJECTFILES=${OBJECTDIR}/ARP.o ${OBJECTDIR}/ICMP.o ${OBJECTDIR}/IP.o ${OBJECTDIR}/dsPicWEB.o ${OBJECTDIR}/enc.o ${OBJECTDIR}/ethernet.o ${OBJECTDIR}/http.o ${OBJECTDIR}/lcd.o ${OBJECTDIR}/memory.o ${OBJECTDIR}/tcp.o ${OBJECTDIR}/tcpip.o

# Source Files
SOURCEFILES=ARP.s ICMP.s IP.s dsPicWEB.s enc.s ethernet.s http.s lcd.s memory.s tcp.s tcpip.s


CFLAGS=
ASFLAGS=
LDLIBSOPTIONS=

############# Tool locations ##########################################
# If you copy a project from one host to another, the path where the  #
# compiler is installed may be different.                             #
# If you open this project with MPLAB X in the new host, this         #
# makefile will be regenerated and the paths will be corrected.       #
#######################################################################
# fixDeps replaces a bunch of sed/cat/printf statements that slow down the build
FIXDEPS=fixDeps

.build-conf:  ${BUILD_SUBPROJECTS}
ifneq ($(INFORMATION_MESSAGE), )
	@echo $(INFORMATION_MESSAGE)
endif
	${MAKE}  -f nbproject/Makefile-default.mk dist/${CND_CONF}/${IMAGE_TYPE}/15.Ethernet.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}

MP_PROCESSOR_OPTION=30F2011
MP_LINKER_FILE_OPTION=,--script=p30F2011.gld
# ------------------------------------------------------------------------------------
# Rules for buildStep: compile
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
else
endif

# ------------------------------------------------------------------------------------
# Rules for buildStep: assemble
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
${OBJECTDIR}/ARP.o: ARP.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/ARP.o.d 
	@${RM} ${OBJECTDIR}/ARP.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  ARP.s  -o ${OBJECTDIR}/ARP.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -D__DEBUG -D__MPLAB_DEBUGGER_SIMULATOR=1  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/ARP.o.d",--defsym=__MPLAB_BUILD=1,--defsym=__MPLAB_DEBUG=1,--defsym=__DEBUG=1,--defsym=__MPLAB_DEBUGGER_SIMULATOR=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/ARP.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
${OBJECTDIR}/ICMP.o: ICMP.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/ICMP.o.d 
	@${RM} ${OBJECTDIR}/ICMP.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  ICMP.s  -o ${OBJECTDIR}/ICMP.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -D__DEBUG -D__MPLAB_DEBUGGER_SIMULATOR=1  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/ICMP.o.d",--defsym=__MPLAB_BUILD=1,--defsym=__MPLAB_DEBUG=1,--defsym=__DEBUG=1,--defsym=__MPLAB_DEBUGGER_SIMULATOR=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/ICMP.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
${OBJECTDIR}/IP.o: IP.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/IP.o.d 
	@${RM} ${OBJECTDIR}/IP.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  IP.s  -o ${OBJECTDIR}/IP.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -D__DEBUG -D__MPLAB_DEBUGGER_SIMULATOR=1  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/IP.o.d",--defsym=__MPLAB_BUILD=1,--defsym=__MPLAB_DEBUG=1,--defsym=__DEBUG=1,--defsym=__MPLAB_DEBUGGER_SIMULATOR=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/IP.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
${OBJECTDIR}/dsPicWEB.o: dsPicWEB.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/dsPicWEB.o.d 
	@${RM} ${OBJECTDIR}/dsPicWEB.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  dsPicWEB.s  -o ${OBJECTDIR}/dsPicWEB.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -D__DEBUG -D__MPLAB_DEBUGGER_SIMULATOR=1  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/dsPicWEB.o.d",--defsym=__MPLAB_BUILD=1,--defsym=__MPLAB_DEBUG=1,--defsym=__DEBUG=1,--defsym=__MPLAB_DEBUGGER_SIMULATOR=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/dsPicWEB.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
${OBJECTDIR}/enc.o: enc.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/enc.o.d 
	@${RM} ${OBJECTDIR}/enc.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  enc.s  -o ${OBJECTDIR}/enc.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -D__DEBUG -D__MPLAB_DEBUGGER_SIMULATOR=1  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/enc.o.d",--defsym=__MPLAB_BUILD=1,--defsym=__MPLAB_DEBUG=1,--defsym=__DEBUG=1,--defsym=__MPLAB_DEBUGGER_SIMULATOR=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/enc.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
${OBJECTDIR}/ethernet.o: ethernet.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/ethernet.o.d 
	@${RM} ${OBJECTDIR}/ethernet.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  ethernet.s  -o ${OBJECTDIR}/ethernet.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -D__DEBUG -D__MPLAB_DEBUGGER_SIMULATOR=1  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/ethernet.o.d",--defsym=__MPLAB_BUILD=1,--defsym=__MPLAB_DEBUG=1,--defsym=__DEBUG=1,--defsym=__MPLAB_DEBUGGER_SIMULATOR=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/ethernet.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
${OBJECTDIR}/http.o: http.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/http.o.d 
	@${RM} ${OBJECTDIR}/http.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  http.s  -o ${OBJECTDIR}/http.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -D__DEBUG -D__MPLAB_DEBUGGER_SIMULATOR=1  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/http.o.d",--defsym=__MPLAB_BUILD=1,--defsym=__MPLAB_DEBUG=1,--defsym=__DEBUG=1,--defsym=__MPLAB_DEBUGGER_SIMULATOR=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/http.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
${OBJECTDIR}/lcd.o: lcd.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/lcd.o.d 
	@${RM} ${OBJECTDIR}/lcd.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  lcd.s  -o ${OBJECTDIR}/lcd.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -D__DEBUG -D__MPLAB_DEBUGGER_SIMULATOR=1  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/lcd.o.d",--defsym=__MPLAB_BUILD=1,--defsym=__MPLAB_DEBUG=1,--defsym=__DEBUG=1,--defsym=__MPLAB_DEBUGGER_SIMULATOR=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/lcd.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
${OBJECTDIR}/memory.o: memory.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/memory.o.d 
	@${RM} ${OBJECTDIR}/memory.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  memory.s  -o ${OBJECTDIR}/memory.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -D__DEBUG -D__MPLAB_DEBUGGER_SIMULATOR=1  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/memory.o.d",--defsym=__MPLAB_BUILD=1,--defsym=__MPLAB_DEBUG=1,--defsym=__DEBUG=1,--defsym=__MPLAB_DEBUGGER_SIMULATOR=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/memory.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
${OBJECTDIR}/tcp.o: tcp.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/tcp.o.d 
	@${RM} ${OBJECTDIR}/tcp.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  tcp.s  -o ${OBJECTDIR}/tcp.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -D__DEBUG -D__MPLAB_DEBUGGER_SIMULATOR=1  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/tcp.o.d",--defsym=__MPLAB_BUILD=1,--defsym=__MPLAB_DEBUG=1,--defsym=__DEBUG=1,--defsym=__MPLAB_DEBUGGER_SIMULATOR=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/tcp.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
${OBJECTDIR}/tcpip.o: tcpip.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/tcpip.o.d 
	@${RM} ${OBJECTDIR}/tcpip.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  tcpip.s  -o ${OBJECTDIR}/tcpip.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -D__DEBUG -D__MPLAB_DEBUGGER_SIMULATOR=1  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/tcpip.o.d",--defsym=__MPLAB_BUILD=1,--defsym=__MPLAB_DEBUG=1,--defsym=__DEBUG=1,--defsym=__MPLAB_DEBUGGER_SIMULATOR=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/tcpip.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
else
${OBJECTDIR}/ARP.o: ARP.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/ARP.o.d 
	@${RM} ${OBJECTDIR}/ARP.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  ARP.s  -o ${OBJECTDIR}/ARP.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/ARP.o.d",--defsym=__MPLAB_BUILD=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/ARP.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
${OBJECTDIR}/ICMP.o: ICMP.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/ICMP.o.d 
	@${RM} ${OBJECTDIR}/ICMP.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  ICMP.s  -o ${OBJECTDIR}/ICMP.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/ICMP.o.d",--defsym=__MPLAB_BUILD=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/ICMP.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
${OBJECTDIR}/IP.o: IP.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/IP.o.d 
	@${RM} ${OBJECTDIR}/IP.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  IP.s  -o ${OBJECTDIR}/IP.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/IP.o.d",--defsym=__MPLAB_BUILD=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/IP.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
${OBJECTDIR}/dsPicWEB.o: dsPicWEB.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/dsPicWEB.o.d 
	@${RM} ${OBJECTDIR}/dsPicWEB.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  dsPicWEB.s  -o ${OBJECTDIR}/dsPicWEB.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/dsPicWEB.o.d",--defsym=__MPLAB_BUILD=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/dsPicWEB.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
${OBJECTDIR}/enc.o: enc.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/enc.o.d 
	@${RM} ${OBJECTDIR}/enc.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  enc.s  -o ${OBJECTDIR}/enc.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/enc.o.d",--defsym=__MPLAB_BUILD=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/enc.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
${OBJECTDIR}/ethernet.o: ethernet.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/ethernet.o.d 
	@${RM} ${OBJECTDIR}/ethernet.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  ethernet.s  -o ${OBJECTDIR}/ethernet.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/ethernet.o.d",--defsym=__MPLAB_BUILD=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/ethernet.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
${OBJECTDIR}/http.o: http.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/http.o.d 
	@${RM} ${OBJECTDIR}/http.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  http.s  -o ${OBJECTDIR}/http.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/http.o.d",--defsym=__MPLAB_BUILD=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/http.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
${OBJECTDIR}/lcd.o: lcd.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/lcd.o.d 
	@${RM} ${OBJECTDIR}/lcd.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  lcd.s  -o ${OBJECTDIR}/lcd.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/lcd.o.d",--defsym=__MPLAB_BUILD=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/lcd.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
${OBJECTDIR}/memory.o: memory.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/memory.o.d 
	@${RM} ${OBJECTDIR}/memory.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  memory.s  -o ${OBJECTDIR}/memory.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/memory.o.d",--defsym=__MPLAB_BUILD=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/memory.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
${OBJECTDIR}/tcp.o: tcp.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/tcp.o.d 
	@${RM} ${OBJECTDIR}/tcp.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  tcp.s  -o ${OBJECTDIR}/tcp.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/tcp.o.d",--defsym=__MPLAB_BUILD=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/tcp.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
${OBJECTDIR}/tcpip.o: tcpip.s  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/tcpip.o.d 
	@${RM} ${OBJECTDIR}/tcpip.o 
	${MP_CC} $(MP_EXTRA_AS_PRE)  tcpip.s  -o ${OBJECTDIR}/tcpip.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -omf=elf   -D__30F2011 -Wa,-MD,"${OBJECTDIR}/tcpip.o.d",--defsym=__MPLAB_BUILD=1,-g,--no-relax$(MP_EXTRA_AS_POST)
	@${FIXDEPS} "${OBJECTDIR}/tcpip.o.d"  $(SILENT)  -rsi ${MP_CC_DIR}../  
	
endif

# ------------------------------------------------------------------------------------
# Rules for buildStep: assemblePreproc
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
else
endif

# ------------------------------------------------------------------------------------
# Rules for buildStep: link
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
dist/${CND_CONF}/${IMAGE_TYPE}/15.Ethernet.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}: ${OBJECTFILES}  nbproject/Makefile-${CND_CONF}.mk    
	@${MKDIR} dist/${CND_CONF}/${IMAGE_TYPE} 
	${MP_CC} $(MP_EXTRA_LD_PRE)  -o dist/${CND_CONF}/${IMAGE_TYPE}/15.Ethernet.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}  ${OBJECTFILES_QUOTED_IF_SPACED}      -mcpu=$(MP_PROCESSOR_OPTION)        -D__DEBUG -D__MPLAB_DEBUGGER_SIMULATOR=1  -omf=elf   -Wl,,--defsym=__MPLAB_BUILD=1,--defsym=__MPLAB_DEBUG=1,--defsym=__DEBUG=1,--defsym=__MPLAB_DEBUGGER_SIMULATOR=1,$(MP_LINKER_FILE_OPTION),--stack=16,--check-sections,--data-init,--pack-data,--handles,--isr,--no-gc-sections,--fill-upper=0,--stackguard=16,--no-force-link,--smart-io,-Map="${DISTDIR}/${PROJECTNAME}.${IMAGE_TYPE}.map",--report-mem$(MP_EXTRA_LD_POST) 
	
else
dist/${CND_CONF}/${IMAGE_TYPE}/15.Ethernet.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}: ${OBJECTFILES}  nbproject/Makefile-${CND_CONF}.mk   
	@${MKDIR} dist/${CND_CONF}/${IMAGE_TYPE} 
	${MP_CC} $(MP_EXTRA_LD_PRE)  -o dist/${CND_CONF}/${IMAGE_TYPE}/15.Ethernet.X.${IMAGE_TYPE}.${DEBUGGABLE_SUFFIX}  ${OBJECTFILES_QUOTED_IF_SPACED}      -mcpu=$(MP_PROCESSOR_OPTION)        -omf=elf   -Wl,,--defsym=__MPLAB_BUILD=1,$(MP_LINKER_FILE_OPTION),--stack=16,--check-sections,--data-init,--pack-data,--handles,--isr,--no-gc-sections,--fill-upper=0,--stackguard=16,--no-force-link,--smart-io,-Map="${DISTDIR}/${PROJECTNAME}.${IMAGE_TYPE}.map",--report-mem$(MP_EXTRA_LD_POST) 
	${MP_CC_DIR}\\xc16-bin2hex dist/${CND_CONF}/${IMAGE_TYPE}/15.Ethernet.X.${IMAGE_TYPE}.${DEBUGGABLE_SUFFIX} -a  -omf=elf  
	
endif


# Subprojects
.build-subprojects:


# Subprojects
.clean-subprojects:

# Clean Targets
.clean-conf: ${CLEAN_SUBPROJECTS}
	${RM} -r build/default
	${RM} -r dist/default

# Enable dependency checking
.dep.inc: .depcheck-impl

DEPFILES=$(shell mplabwildcard ${POSSIBLE_DEPFILES})
ifneq (${DEPFILES},)
include ${DEPFILES}
endif
